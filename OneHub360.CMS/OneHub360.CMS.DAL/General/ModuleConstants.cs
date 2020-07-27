using System;
using System.Web.Hosting;

namespace OneHub360.CMS.DAL
{
    public struct ModuleConstants
    {


        public static RepositoryMode RepositoryMode
        {
            get { return RepositoryMode.SQLServer; }
        }

        public static string DocumentIdProperty { get { return "_CMSDOCID"; } }

        public static string TemplateIdProperty { get { return "_CMSTemplateID"; } }

        public static long YammerDemoAccount { get { return 1606622398; } }

        public struct FileNet
        {
            public static string Connection = "http://winp8:9080/wsi/FNCEWS40MTOM/";
            public static string Domain = "p8admin";
            public static string Folder = "/OneHub360";
            public static string CalssId = "OneHub360Document";//"{5BE8D278-BFDD-4AEC-AD37-A9219E23D43B}";
            public static string ObjectStoreName = "OBJ";
            public static string Username = "administrator";
            public static string Password = "Ebla1234";
        }
        public struct Demo
        {
            public static string SMSAlertNumber = "96590084822"; // "96599933830";
            public static bool SendSMS = false;
            public static long ChatId = 173882779;
        }

        public static Guid DraftMemoType = new Guid("23722766-6DD5-4CB3-8989-22655038300E");
        public static Guid AttachedDraftMemoType = new Guid("378F1E1B-6235-4A6D-BC01-6D3448C23A00");
        public static Guid IncomingMemoType = new Guid("98b2986d-542d-4959-9317-3f3b398ccf3e");
        public static Guid OutgoingMemoType = new Guid("38C15649-3D62-42A7-8B53-979A763F4055");
        public static Guid MemoDocumentTemplate = new Guid("153C9F52-C402-46B0-AC2A-F3D69812301C");

        public static Guid DraftLetterType = new Guid("3D2ABCA4-40DD-4B07-8782-B287F2002CD3");
        public static Guid AttachedDraftLetterType = new Guid("A1966D76-1172-467B-A5F3-87D9EF028F75");
        public static Guid IncomingLetterType = new Guid("C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29");
        public static Guid OutgoingLetterType = new Guid("16E26CF7-39DC-4962-859C-B6A20B4D0FFF");
        public static Guid LetterDocumentTemplate = new Guid("E90FA041-883F-4F66-892F-A69001742444");

        public static Guid AttachementDocumentTemplate = new Guid("A7FEF784-AA2E-4E08-8E60-B3F3864042A5");

        // MOI Types

        public static Guid MOIReport = new Guid("10AEFD01-BE05-4333-B160-C47D1D56617B");

        public static string DocumentStartBookMark = "DocumentStart";
        public static string TempFolderPath = HostingEnvironment.MapPath("~/Temp/");
        public static string NumberGeneratorConfigPath = "~/config/modules/cms/NumberGenerator.json";

        //public static string WordViewerUrl = "http://onehub360oos/wv/wordviewerframe.aspx?ui=ar&WOPISrc=";
        //public static string FilePreviewUrl = "http://onehub360oos/wv/ResReader.ashx?n=p1.img&WOPIsrc=";
        //public static string ExcelViewerUrl = "http://onehub360oos/x/_layouts/xlviewerinternal.aspx?ui=ar&WOPISrc=";
        //public static string PowerPointViewerUrl = "http://onehub360oos/p/PowerPointFrame.aspx?ui=ar&WOPISrc=";
        public static string PdfViewUrl = "/components/modules/cms/pdfjs/web/viewer.html";
        //public static string CMSServiceBaseUrl = "http://DESKTOP-THTHN90:363/";

        //public static string PdfConvertionUrl = "http://onehub360oos/wv/WordViewer/request.pdf";

        //public static string NumbersPrefix = "CPA";

        public struct MemoActions
        {
            public static Guid SendDraftMemo = new Guid("96565a61-8913-4ace-8098-a6820143ac5a");
            public static Guid CreateDraftMemo = new Guid("065B28E6-96B4-4F12-91F4-3326EA4D28FB");
            public static Guid ForwardIncomingMemo = new Guid("7AF29394-2243-450F-894C-9A95FB7B7BF7");
        }

        public struct LetterActions
        {

            public static Guid CreateDraftLetter = new Guid("5248412B-9AB5-45DF-8100-9FB8EA7521C9");
            public static Guid SendDraftLetter = new Guid("DD797965-6F99-4DF2-B5AB-09F6FE8B15B6");

        }
    }

    public enum RepositoryMode
    {
        FileNet,
        SQLServer,
        SharePoint
    }
}
