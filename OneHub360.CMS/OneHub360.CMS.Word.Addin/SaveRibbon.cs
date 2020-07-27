using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Office.Core;

namespace OneHub360.CMS.Word.Addin
{
   
    public partial class SaveRibbon
    {
        private const string DocumentIdPropertyName = "_CMSDOCID";
        private const string TemplateIdPropertyName = "_CMSTemplateID";

        private enum Mode
        {
            Document,
            Template,
            Invalid
        }

        private void SaveRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.Application.StatusBar = "Saving....";

            Globals.ThisAddIn.Application.ActiveDocument.Save();

            DocumentProperties properties = (DocumentProperties)Globals.ThisAddIn.Application.ActiveDocument.CustomDocumentProperties;

            var mode = Mode.Invalid;

            if (properties.Cast<DocumentProperty>().Where(c => c.Name == TemplateIdPropertyName).Count() != 0)
            {
                mode = Mode.Template;
            }

            if (properties.Cast<DocumentProperty>().Where(c => c.Name == DocumentIdPropertyName ).Count() != 0)
            {
                mode = Mode.Document;
            }

            if (mode == Mode.Document)
            {
                SaveDocument(properties[DocumentIdPropertyName].Value);
                return;
            }

            if(mode == Mode.Template)
            {
                SaveTemplate(properties[TemplateIdPropertyName].Value);
                return;
            }

            if(mode == Mode.Invalid)
            {
                System.Windows.Forms.MessageBox.Show("The current document is not related to the CMS system");
            }
        }

        private void SaveTemplate(dynamic templateId)
        {
            var tempFilePath = string.Format("{0}{1}.docx", Path.GetTempPath(), Guid.NewGuid().ToString());

            File.Copy(Globals.ThisAddIn.Application.ActiveDocument.FullName, tempFilePath);

            var contents = Convert.ToBase64String(File.ReadAllBytes(tempFilePath));


            var webClient = new HttpClient();

            JObject postData = new JObject();

            postData.Add("contents", contents);
            postData.Add("id", templateId);

            StringContent postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var result = webClient.PostAsync(Properties.Settings.Default.CMSServiceBaseUrl + "cms/template/word/update", postContent).Result;

            Globals.ThisAddIn.Application.StatusBar = "Template Saved";
        }

        private void SaveDocument(dynamic documentId)
        {
            var tempFilePath = string.Format("{0}{1}.docx", Path.GetTempPath(), Guid.NewGuid().ToString());

            File.Copy(Globals.ThisAddIn.Application.ActiveDocument.FullName, tempFilePath);

            var contents = Convert.ToBase64String(File.ReadAllBytes(tempFilePath));


            var webClient = new HttpClient();

            JObject postData = new JObject();

            postData.Add("contents", contents);
            postData.Add("id", documentId);

            StringContent postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var result = webClient.PostAsync(Properties.Settings.Default.CMSServiceBaseUrl + "cms/document/word/update", postContent).Result;

            Globals.ThisAddIn.Application.StatusBar = "Document Saved";
        }
    }
}
