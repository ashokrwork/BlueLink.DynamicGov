namespace OneHub360.CMS.DAL
{
    public class CheckFileInfoResponse
    {
        public string BaseFileName { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public int Size { get; set; }
        public string Version { get; set; }
        public string BreadcrumbBrandName { get; set; }
        public string BreadcrumbBrandUrl { get; set; }
        public string BreadcrumbFolderName { get; set; }
        public string BreadcrumbFolderUrl { get; set; }
        public string BreadcrumbDocName { get; set; }
        public string BreadcrumbDocUrl { get; set; }
        public bool UserCanWrite { get; set; }
        public bool ReadOnly { get; set; }
        public bool SupportsUpdate { get; set; }
        public bool UserCanNotWriteRelative { get; set; }
        public string UserFriendlyName { get; set; }
        public bool AllowExternalMarketplace { get; set; }
        public string ClientUrl { get; set; }
        public bool CloseButtonClosesWindow { get; set; }
        public string CloseUrl { get; set; }
        public bool DisablePrint { get; set; }
        public bool DisableTranslation { get; set; }
        public string DownloadUrl { get; set; }
        public string FileSharingUrl { get; set; }
        public bool SupportsScenarioLinks { get; set; }
        public bool ProtectInClient { get; set; }
        public bool RestrictedWebViewOnly { get; set; }
        public string HelpUrl { get; set; }
        public string FileUrl { get; set; }
        public string UserPrincipalName { get; set; }
        public string LastModifiedTime { get; set; }
        public bool SupportsCoauth { get; set; }
        public bool SupportsCobalt { get; set; }
        public bool LicenseCheckForEditIsEnabled { get; set; }
        public string FileExtension { get; set; }

        public bool SupportsGetLock { get { return true; } }
        public bool SupportsLocks { get { return true; } }

        public string HostAuthenticationId { get; set; }
    }
}
