using OneHub360.DB;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Linq;
using A = DocumentFormat.OpenXml.Drawing;
using System.Reflection;
using System;
using DocumentFormat.OpenXml;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.IO.Compression;
using OneHub360.CMS.DAL.DigitalSignature;
using FileNet.Api.Core;
using Microsoft.Web.Services3.Security.Tokens;
using FileNet.Api.Collection;
using FileNet.Api.Constants;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using System.Xml.XPath;
using System.Xml;
using System.IO.Packaging;
using System.Text;

namespace OneHub360.CMS.DAL
{
    public abstract class DocumentRepositoryFactory : NHEntityRepository<Document>
    {
        public DocumentRepositoryFactory(IDBContext context) : base(context)
        {
        }

        

        public static DocumentRepositoryFactory GetInstance(IDBContext context)
        {
            //if (ModuleConstants.RepositoryMode == RepositoryMode.FileNet)
            //    return new FileNetDocumentRepository(context);
            //else
                return new SQLServerDocumentRepository(context);

        }

        public abstract CheckFileInfoResponse GetFileInfo(Guid documentId);
        public abstract byte[] GetFileContent(Guid documentId);

        public abstract void SaveFileContent(Guid documentId,byte[] fileContent);

        public abstract void UpdateDocument(Document document);
        public abstract Document InsertDocument(Document document);


        public byte[] ZipDocuments(IList<Document> documents)
        {
            var zipStream = new MemoryStream();

            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var document in documents)
                {
                    var documentFile = archive.CreateEntry(string.Format("{1}-{0}{2}", document.Title,documents.IndexOf(document) + 1, Path.GetExtension(document.FileName)), CompressionLevel.NoCompression);

                    using (var entyrStream = documentFile.Open())
                    {
                        entyrStream.Write(document.FileBinary, 0, document.FileBinary.Length);
                    }
                }
            }

            return zipStream.ToArray();
        }
        
        public Document GetFromTemplate(Template template, CreationInfo creationInfo)
        {
            var document = InitEntity(creationInfo);

            document.FileBinary = template.File;
            document.FileName = template.Filename;
            document.PagesCount = 1;
            document.FK_Template = template.Id;
            document.Signed = false;
            document.Title = template.Title;

            return document;
        }
        public void ConvertToPdf(Document document)
        {
            var tempWordFilePath = string.Format("{0}{1}.docx", ModuleConstants.TempFolderPath, document.Id);

            File.WriteAllBytes(tempWordFilePath, document.FileBinary);

            var tempFileWopiUrl = HttpUtility.UrlEncode(string.Format("{0}{1}{2}.docx", CMSConfigLoader.Generator.configData.CMSServiceBaseUrl, "wopi/files/", document.Id));
            var pdfConversionUrl = string.Format("{0}?WOPIsrc={1}&type=accesspdf&z={2}", CMSConfigLoader.Generator.configData.PdfConvertionUrl, tempFileWopiUrl, document.LastModified);

            using (var webClient = new WebClient())
            {
                document.FileBinary = webClient.DownloadData(new Uri(pdfConversionUrl));
                document.FileName = Path.ChangeExtension(document.FileName, ".pdf");
            }

            File.Delete(tempWordFilePath);

        }

        
        public void DigitallySignDocument(X509Certificate2 certificate, Document document, string senderName, DateTime actionDate,string senderid)
        {
            if(certificate != null)
                FileSignManager.SignPdfDocument(document, certificate, senderName, actionDate);
        }
        #region OpenXml
        public void DeleteComments(Document document)
        {
            using (var fileStream = new MemoryStream())
            {
                fileStream.Write(document.FileBinary, 0, document.FileBinary.Length);

                using (WordprocessingDocument wpDocument = WordprocessingDocument.Open(fileStream, true))
                {
                    DeleteDocumentComments(wpDocument);
                    wpDocument.Close();
                }

                using (var resultStream = new MemoryStream())
                {
                    fileStream.WriteTo(resultStream);

                    document.FileBinary = resultStream.ToArray();
                }
            }
        }

        public void UpdateDocxPlaceHolders(Document document, dynamic metaData)
        {
            using (var fileStream = new MemoryStream())
            {
                fileStream.Write(document.FileBinary, 0, document.FileBinary.Length);

                using (WordprocessingDocument wpDocument = WordprocessingDocument.Open(fileStream, true))
                {
                    PropertyInfo[] propertyInfos;
                    propertyInfos = metaData.GetType().GetProperties();

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        try
                        {
                            if (propertyInfo.PropertyType == typeof(string))
                            {
                                try { ModifyDocumentPlaceHolder(wpDocument.MainDocumentPart.Document, propertyInfo.Name, (string)propertyInfo.GetValue(metaData), false); }
                                catch { }
                                try { ModifyHeaderPlaceHolder(wpDocument.MainDocumentPart.Document, propertyInfo.Name, (string)propertyInfo.GetValue(metaData), false); }
                                catch { }   
                            }
                            else if (propertyInfo.PropertyType == typeof(byte[]))
                                ModifyDocumentPlaceHolder(wpDocument.MainDocumentPart.Document, propertyInfo.Name, (byte[])propertyInfo.GetValue(metaData), false);

                        }
                        catch { }
                    }

                    wpDocument.Close();
                }

                using (var resultStream = new MemoryStream())
                {
                    fileStream.WriteTo(resultStream);

                    document.FileBinary = resultStream.ToArray();
                }
            }
        }

        public byte[] WordOpenXmlToBinary(string wordOpenXML)
        {
            string packageXmlns = "http://schemas.microsoft.com/office/2006/xmlPackage";
            

            byte[] result;

            using (var stream = new MemoryStream())
            {
                Package newPkg = Package.Open(stream, FileMode.Create);
                XPathDocument xpDocument = new XPathDocument(new StringReader(wordOpenXML));
                XPathNavigator xpNavigator = xpDocument.CreateNavigator();

                XmlNamespaceManager nsManager = new XmlNamespaceManager(xpNavigator.NameTable);
                nsManager.AddNamespace("pkg", packageXmlns);
                XPathNodeIterator xpIterator = xpNavigator.Select("//pkg:part", nsManager);

                while (xpIterator.MoveNext())
                {
                    Uri partUri = new Uri(xpIterator.Current.GetAttribute("name", packageXmlns), UriKind.Relative);

                    PackagePart pkgPart = newPkg.CreatePart(partUri, xpIterator.Current.GetAttribute("contentType", packageXmlns));

                    // Set this package part's contents to this XML node's inner XML, sans its surrounding xmlData element.
                    string strInnerXml = xpIterator.Current.InnerXml
                        .Replace("<pkg:xmlData xmlns:pkg=\"" + packageXmlns + "\">", "")
                        .Replace("</pkg:xmlData>", "");
                    byte[] buffer = Encoding.UTF8.GetBytes(strInnerXml);
                    pkgPart.GetStream().Write(buffer, 0, buffer.Length);
                }

                newPkg.Flush();
                newPkg.Close();

                result = stream.ToArray();
            }

            return result;
        }
        public void AddTextAfterBookMark(Document document, string bookMarkName, string text)
        {

            using (var fileStream = new MemoryStream())
            {
                fileStream.Write(document.FileBinary, 0, document.FileBinary.Length);


                using (WordprocessingDocument wpDocument = WordprocessingDocument.Open(fileStream, true))
                {
                    var body = wpDocument.MainDocumentPart.Document.GetFirstChild<Body>();
                    var paras = body.Elements<Paragraph>();

                    //Iterate through the paragraphs to find the bookmarks inside
                    foreach (var para in paras)
                    {
                        var bookMarkStarts = para.Elements<BookmarkStart>();
                        var bookMarkEnds = para.Elements<BookmarkEnd>();


                        foreach (BookmarkStart bookMarkStart in bookMarkStarts)
                        {
                            if (bookMarkStart.Name == bookMarkName)
                            {
                                //Get the id of the bookmark start to find the bookmark end
                                var id = bookMarkStart.Id.Value;
                                var bookmarkEnd = bookMarkEnds.Where(i => i.Id.Value == id).First();

                                var runElement = CreateRunTextWithStyle(text, "Arial", 18);

                                para.InsertAfter(runElement, bookmarkEnd);

                            }
                        }
                    }
                    wpDocument.Close();
                }

                using (var resultStream = new MemoryStream())
                {
                    fileStream.WriteTo(resultStream);

                    document.FileBinary = resultStream.ToArray();
                }
            }
        }

        private Run CreateRunTextWithStyle(string input, string fontFamily, int fontSize)
        {

            var wordFontSize = (fontSize * 2).ToString(); // Half-point font size 

            string[] lines = input.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var run = new Run();

            foreach (string line in lines) // Add text to run.
            {
                Text currLine = new Text(line);

                run.AppendChild(currLine);
                run.AppendChild(new CarriageReturn());
                run.AppendChild(new CarriageReturn());
            }

            RunProperties runProp = new RunProperties(); // Create run properties.
            RunFonts runFont = new RunFonts();           // Create font
            runFont.Ascii = fontFamily;                     // Specify font family

            FontSize size = new FontSize();
            size.Val = new StringValue(wordFontSize);
            runProp.Append(runFont);
            runProp.Append(size);

            run.PrependChild(runProp);

            return run;
        }


        
        private void ModifyDocumentPlaceHolder(DocumentFormat.OpenXml.Wordprocessing.Document document, string tagName, byte[] value, bool removePlaceholder)
        {

            var placeHolders = document.MainDocumentPart.RootElement.
                    Descendants<SdtElement>().
                    Where(P => P.Elements<SdtProperties>().FirstOrDefault().
                        Elements<Tag>().FirstOrDefault() != null);

            // Select the required element by tag name
            var cc = placeHolders.FirstOrDefault(P =>
                P.Elements<SdtProperties>().FirstOrDefault().
                Elements<Tag>().FirstOrDefault().Val == tagName);


            string embed = null;
            if (cc != null)
            {
                Drawing dr = cc.Descendants<Drawing>().FirstOrDefault();
                if (dr != null)
                {
                    var blip = dr.Descendants<A.Blip>().FirstOrDefault();
                    if (blip != null)
                        embed = blip.Embed;
                }
            }
            if (embed != null)
            {
                IdPartPair idpp = document.MainDocumentPart.Parts
                    .Where(pa => pa.RelationshipId == embed).FirstOrDefault();
                if (idpp != null)
                {
                    var ip = (ImagePart)idpp.OpenXmlPart;
                    using (MemoryStream imageStream = new MemoryStream(value))
                    {
                        ip.FeedData(imageStream);
                    }
                }
            }

        }

        private void ModifyHeaderPlaceHolder(DocumentFormat.OpenXml.Wordprocessing.Document document, string tagName, string value, bool removePlaceholder)
        {

            // Find all placeholders with Tag name
            foreach (HeaderPart part in document.MainDocumentPart.HeaderParts)
            {
                var placeHolders = part.RootElement.
                    Descendants<SdtElement>().
                    Where(P => P.Elements<SdtProperties>().FirstOrDefault().
                        Elements<Tag>().FirstOrDefault() != null);

                // Select the required element by tag name
                var matchedPlaceHolders = placeHolders.Where(P =>
                 P.Elements<SdtProperties>().FirstOrDefault().
                 Elements<Tag>().FirstOrDefault().Val == tagName);

                foreach (SdtElement placeHolder in matchedPlaceHolders)
                {
                    // Remove all text from the place holder
                    placeHolder.Descendants<Text>().ToList().ForEach(
                        delegate (Text textObj)
                        {
                            textObj.Text = string.Empty;
                        });

                    // Add the required text
                    placeHolder.Descendants<Text>().FirstOrDefault().Text = value;
                }

                //if(removePlaceholder)
                //{
                //    foreach (var elem in requiredPlaceHolder.Elements())
                //    {
                //        requiredPlaceHolder.Parent.InsertBefore(elem.CloneNode(true), requiredPlaceHolder);
                //    }

                //    requiredPlaceHolder.Remove();

                //}
            }

        }

        private void ModifyDocumentPlaceHolder(DocumentFormat.OpenXml.Wordprocessing.Document document, string tagName, string value, bool removePlaceholder)
        {

            // Find all placeholders with Tag name
            var placeHolders = document.MainDocumentPart.RootElement.
                Descendants<SdtElement>().
                Where(P => P.Elements<SdtProperties>().FirstOrDefault().
                    Elements<Tag>().FirstOrDefault() != null);

            // Select the required element by tag name
            var matchedPlaceHolders = placeHolders.Where(P =>
                P.Elements<SdtProperties>().FirstOrDefault().
                Elements<Tag>().FirstOrDefault().Val == tagName);

            foreach (SdtElement placeHolder in matchedPlaceHolders)
            {
                // Remove all text from the place holder
                placeHolder.Descendants<Text>().ToList().ForEach(
                    delegate (Text textObj)
                    {
                        textObj.Text = string.Empty;
                    });

                // Add the required text
                placeHolder.Descendants<Text>().FirstOrDefault().Text = value;
            }

        }

        private void DeleteDocumentComments(WordprocessingDocument document)
        {
            // Set commentPart to the document WordprocessingCommentsPart, 
            // if it exists.
            WordprocessingCommentsPart commentPart =
                document.MainDocumentPart.WordprocessingCommentsPart;

            // If no WordprocessingCommentsPart exists, there can be no 
            // comments. Stop execution and return from the method.
            if (commentPart == null)
            {
                return;
            }

            // Create a list of comments by the specified author, or
            // if the author name is empty, all authors.
            List<Comment> commentsToDelete =
                commentPart.Comments.Elements<Comment>().ToList();

            IEnumerable<string> commentIds =
                commentsToDelete.Select(r => r.Id.Value);

            // Delete each comment in commentToDelete from the 
            // Comments collection.
            foreach (Comment c in commentsToDelete)
            {
                c.Remove();
            }

            // Save the comment part change.
            commentPart.Comments.Save();

            var doc = document.MainDocumentPart.Document;

            // Delete CommentRangeStart for each
            // deleted comment in the main document.
            List<CommentRangeStart> commentRangeStartToDelete =
                doc.Descendants<CommentRangeStart>().
                Where(c => commentIds.Contains(c.Id.Value)).ToList();
            foreach (CommentRangeStart c in commentRangeStartToDelete)
            {
                c.Remove();
            }

            // Delete CommentRangeEnd for each deleted comment in the main document.
            List<CommentRangeEnd> commentRangeEndToDelete =
                doc.Descendants<CommentRangeEnd>().
                Where(c => commentIds.Contains(c.Id.Value)).ToList();
            foreach (CommentRangeEnd c in commentRangeEndToDelete)
            {
                c.Remove();
            }

            // Delete CommentReference for each deleted comment in the main document.
            List<CommentReference> commentRangeReferenceToDelete =
                doc.Descendants<CommentReference>().
                Where(c => commentIds.Contains(c.Id.Value)).ToList();
            foreach (CommentReference c in commentRangeReferenceToDelete)
            {
                c.Remove();
            }
        }

        #endregion
    }
}
