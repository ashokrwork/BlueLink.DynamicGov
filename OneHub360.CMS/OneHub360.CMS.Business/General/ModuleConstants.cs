using System;

namespace OneHub360.CMS.Business
{
    public struct ModuleConstants
    {
        public static Guid DraftMemoType = new Guid("23722766-6DD5-4CB3-8989-22655038300E");
        public static Guid IncomingMemoType = new Guid("98b2986d-542d-4959-9317-3f3b398ccf3e");
        public static Guid OutgoingMemoType = new Guid("38C15649-3D62-42A7-8B53-979A763F4055");
        public static Guid MemoDocumentTemplate = new Guid("153C9F52-C402-46B0-AC2A-F3D69812301C");
        public static Guid AttachementDocumentTemplate = new Guid("A7FEF784-AA2E-4E08-8E60-B3F3864042A5");
        public static string DocumentStartBookMark = "DocumentStart";
        public static string TempFolderVirtualPath = "~/Temp/";
        public static string NumberGeneratorConfigPath = "~/NumberGenerator.json";
    }
}
