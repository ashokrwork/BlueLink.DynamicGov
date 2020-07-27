using System;
using System.Threading.Tasks;
using NWebDav.Server;
using NWebDav.Server.Http;
using NWebDav.Server.Stores;
using NWebDav.Server.AspNet;
using NWebDav.Server.Locking;
using NWebDav.Server.Props;
using System.IO;
using NWebDav.Server.Helpers;
using System.Security.Cryptography;

namespace OneHub360.CMS.Services.WOPI.Word
{
    public class OfficeWebDav : WebDavHandlerFactory
    {
        public OfficeWebDav() : base(GetWebDavDispatcher())
        {
        }

        private static IWebDavDispatcher GetWebDavDispatcher()
        {
            // Create a request handler factory that uses basic authentication
            var requestHandlerFactory = new RequestHandlerFactory();

            // Create a WebDAV dispatcher for the home folder
            var homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return new WebDavDispatcher(new DiskStore(homeFolder), requestHandlerFactory);
        }
    }

    public class SQLStore : IStore
    {
        public Task<IStoreCollection> GetCollectionAsync(Uri uri, IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task<IStoreItem> GetItemAsync(Uri uri, IHttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }

    public class SQLStoreItem : IStoreItem
    {
        private readonly FileInfo _fileInfo;

        public SQLStoreItem(ILockingManager lockingManager, FileInfo fileInfo, bool isWritable)
        {
            LockingManager = lockingManager;
            _fileInfo = fileInfo;
            IsWritable = isWritable;
        }

        public static PropertyManager<SQLStoreItem> DefaultPropertyManager { get; } = new PropertyManager<SQLStoreItem>(new DavProperty<SQLStoreItem>[]
        {
            // RFC-2518 properties
            new DavCreationDate<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.CreationTimeUtc,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.CreationTimeUtc = value;
                    return DavStatusCode.Ok;
                }
            },
            new DavDisplayName<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.Name
            },
            new DavGetContentLength<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.Length
            },
            new DavGetContentType<SQLStoreItem>
            {
                Getter = (context, item) => item.DetermineContentType()
            },
            new DavGetEtag<SQLStoreItem>
            {
                // Calculating the Etag is an expensive operation,
                // because we need to scan the entire file.
                IsExpensive = true,
                Getter = (context, item) => item.CalculateEtag()
            },
            new DavGetLastModified<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.LastWriteTimeUtc,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.LastWriteTimeUtc = value;
                    return DavStatusCode.Ok;
                }
            },
            new DavGetResourceType<SQLStoreItem>
            {
                Getter = (context, item) => null
            },

            // Default locking property handling via the LockingManager
            new DavLockDiscoveryDefault<SQLStoreItem>(),
            new DavSupportedLockDefault<SQLStoreItem>(),

            // Hopmann/Lippert collection properties
            // (although not a collection, the IsHidden property might be valuable)
            new DavExtCollectionIsHidden<SQLStoreItem>
            {
                Getter = (context, item) => (item._fileInfo.Attributes & FileAttributes.Hidden) != 0
            },

            // Win32 extensions
            new Win32CreationTime<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.CreationTimeUtc,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.CreationTimeUtc = value;
                    return DavStatusCode.Ok;
                }
            },
            new Win32LastAccessTime<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.LastAccessTimeUtc,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.LastAccessTimeUtc = value;
                    return DavStatusCode.Ok;
                }
            },
            new Win32LastModifiedTime<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.LastWriteTimeUtc,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.LastWriteTimeUtc = value;
                    return DavStatusCode.Ok;
                }
            },
            new Win32FileAttributes<SQLStoreItem>
            {
                Getter = (context, item) => item._fileInfo.Attributes,
                Setter = (context, item, value) =>
                {
                    item._fileInfo.Attributes = value;
                    return DavStatusCode.Ok;
                }
            }
        });

        public bool IsWritable { get; }
        public string Name => _fileInfo.Name;
        public string UniqueKey => _fileInfo.FullName;
        public string FullPath => _fileInfo.FullName;
        public Task<Stream> GetReadableStreamAsync(IHttpContext httpContext) => Task.FromResult((Stream)_fileInfo.OpenRead());

        public async Task<DavStatusCode> UploadFromStreamAsync(IHttpContext httpContext, Stream inputStream)
        {
            // Check if the item is writable
            if (!IsWritable)
                return DavStatusCode.Conflict;

            // Copy the stream
            try
            {
                // Copy the information to the destination stream
                using (var outputStream = _fileInfo.OpenWrite())
                {
                    await inputStream.CopyToAsync(outputStream).ConfigureAwait(false);
                }
                return DavStatusCode.Ok;
            }
            catch (IOException ioException)
            {
                return DavStatusCode.InsufficientStorage;
            }
        }

        public IPropertyManager PropertyManager => DefaultPropertyManager;
        public ILockingManager LockingManager { get; }

        public async Task<StoreItemResult> CopyAsync(IStoreCollection destination, string name, bool overwrite, IHttpContext httpContext)
        {
            try
            {
                // If the destination is also a disk-store, then we can use the FileCopy API
                // (it's probably a bit more efficient than copying in C#)
                var diskCollection = destination as DiskStoreCollection;
                if (diskCollection != null)
                {
                    // Check if the collection is writable
                    if (!diskCollection.IsWritable)
                        return new StoreItemResult(DavStatusCode.PreconditionFailed);

                    var destinationPath = Path.Combine(diskCollection.FullPath, name);

                    // Check if the file already exists
                    var fileExists = File.Exists(destinationPath);
                    if (fileExists && !overwrite)
                        return new StoreItemResult(DavStatusCode.PreconditionFailed);

                    // Copy the file
                    File.Copy(_fileInfo.FullName, destinationPath, true);

                    // Return the appropriate status
                    return new StoreItemResult(fileExists ? DavStatusCode.NoContent : DavStatusCode.Created);
                }
                else
                {
                    // Create the item in the destination collection
                    var result = await destination.CreateItemAsync(name, overwrite, httpContext).ConfigureAwait(false);

                    // Check if the item could be created
                    if (result.Item != null)
                    {
                        using (var sourceStream = await GetReadableStreamAsync(httpContext).ConfigureAwait(false))
                        {
                            var copyResult = await result.Item.UploadFromStreamAsync(httpContext, sourceStream).ConfigureAwait(false);
                            if (copyResult != DavStatusCode.Ok)
                                return new StoreItemResult(copyResult, result.Item);
                        }
                    }

                    // Return result
                    return new StoreItemResult(result.Result, result.Item);
                }
            }
            catch (Exception exc)
            {
                
                return new StoreItemResult(DavStatusCode.InternalServerError);
            }
        }

        public override int GetHashCode()
        {
            return _fileInfo.FullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var storeItem = obj as SQLStoreItem;
            if (storeItem == null)
                return false;
            return storeItem._fileInfo.FullName.Equals(_fileInfo.FullName, StringComparison.CurrentCultureIgnoreCase);
        }

        private string DetermineContentType()
        {
            return MimeTypeHelper.GetMimeType(_fileInfo.Name);
        }

        private string CalculateEtag()
        {
            using (var stream = File.OpenRead(_fileInfo.FullName))
            {
                var hash = SHA256.Create().ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}