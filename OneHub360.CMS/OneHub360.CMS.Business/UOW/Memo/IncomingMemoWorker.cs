using OneHub360.App.Shared;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using OneHub360.DB;

namespace OneHub360.CMS.Business
{
    public class IncomingMemoWorker : MemoWorker
    {
        public IncomingMemoWorker(WorkerMode mode) : base(mode)
        {
        }



        public IncomingMemoView GetView(Guid outgoingMemoId)
        {
            IncomingMemoView incomingMemoView;
            using (var context = new CMSContext())
            {
                using (var incomingViewRepository = new IncomingMemoViewRepository(context))
                {
                    incomingMemoView = incomingViewRepository.GetById(outgoingMemoId);
                }

            }
            return incomingMemoView;
        }

        public async Task<Guid> Send(IncomingMemoAction action)
        {
            Guid result;

            if (action.Send)
            {
                result = await ForwardIncomingMemo(action);
            }
            else
            {
                result = await CreateDraftFromIncoming(action);
            }

            return result;
        }

        private async Task<Guid> CreateDraftFromIncoming(IncomingMemoAction action)
        {
            var result = Guid.Empty;

            var actionDate = DateTime.Now;
            var fromUser = new UserInfoAutoComplete();
            var toUser = new UserInfoAutoComplete();

            var feedBatch = new EntitiesBatchInsert();

            var creationInfo = new CreationInfo { CreatedBy = action.FK_From.ToString(), CreationDate = actionDate, IsDeleted = false, Language = Language.AR, LastModified = actionDate };

            using (var feedApiClient = new FeedApiClient())
            {
                toUser = await feedApiClient.GetUser(action.FK_To.ToString());
                if (action.Send)
                    fromUser = await feedApiClient.GetUser(action.FK_From.ToString());
            }

            var templateRepository = new TemplateRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            var draftMemoRepository = new DraftMemoRepository(Context);
            var correspondeceDocumentRepository = new CorrespondenceDocumentsRepository(Context);

            var template = templateRepository.GetById(ModuleConstants.MemoDocumentTemplate);

            var document = documentRepository.GetFromTemplate(template, new CreationInfo());

            documentRepository.InitEntity(document, creationInfo);

            documentRepository.UpdateDocxPlaceHolders(document, 
                new { To = toUser.FullName, Subject = action.Subject, OrgUnit = fromUser.Organization });
            documentRepository.AddTextAfterBookMark(document, ModuleConstants.DocumentStartBookMark, action.Brief);

            var draftMemoId = Guid.NewGuid();

            result = draftMemoId;

            var draftMemo = new DraftMemo
            {
                Subject = action.Subject,
                Brief = action.Brief,
                CreatedBy = action.FK_From.ToString(),
                CreationDate = actionDate,
                From = action.FK_From.ToString(),
                Id = draftMemoId,
                FK_Document = draftMemoId,
                Language = DB.Language.AR,
                LastModified = actionDate,
                ThreadId = action.ThreadId,
                Status = DraftMemoStatus.New,
                To = action.FK_To.ToString(),
                Confidential = false
            };

            draftMemo.AddtionalRecipients = "[]";
            draftMemo.CopyTo = "[]";

            draftMemoRepository.Insert(draftMemo);
            AddMainDocument(draftMemo.Id, document, true);

            var allIncomingFiles = GetAllDocuments(action.FK_IncomingMemoId);

            CopyCorrespondenceAttachements(creationInfo, allIncomingFiles, draftMemo.Id, true);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromDraftMemo(draftMemo);
                if (draftMemo.Confidential.Value)
                    feed.Scope = 0;

                feedBatch.feeds.Add(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);

                userAction.Actor = draftMemo.From;

                userAction.FK_Feed = draftMemo.Id;
                userAction.Destination = draftMemo.To;
                userAction.FK_UserActionType = ModuleConstants.MemoActions.CreateDraftMemo;
                userAction.ThreadId = draftMemo.ThreadId;
                userAction.Subject = draftMemo.Subject;
                feedBatch.actions.Add(userAction);

                await feedApiClient.InsertBatch(feedBatch);
            }
            return result;
        }

        private async Task<Guid> ForwardIncomingMemo(IncomingMemoAction action)
        {
            var result = Guid.Empty;

            var tempSignatureImage = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/signature.png"));

            var actionDate = DateTime.Now;
            var fromUser = new UserInfoAutoComplete();
            var toUser = new UserInfoAutoComplete();
            Signature userSignature;

            var feedBatch = new EntitiesBatchInsert();

            var signatureImage = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/signature.png"));

            var outgoingNumber = string.Format("{0}-{1}", CMSConfigLoader.Generator.configData.NumbersPrefix, NumberGenerator.Generator.MemoOutgoingNumber.ToString().PadLeft(5, '0'));
            var incomingNumber = string.Format("{0}-{1}", CMSConfigLoader.Generator.configData.NumbersPrefix, NumberGenerator.Generator.MemoIncomingNumber.ToString().PadLeft(5, '0'));

            var creationInfo = new CreationInfo { CreatedBy = action.FK_From.ToString(), CreationDate = actionDate, IsDeleted = false, Language = Language.AR, LastModified = actionDate };

            using (var feedApiClient = new FeedApiClient())
            {
                toUser = await feedApiClient.GetUser(action.FK_To.ToString());
                if (action.Send)
                    fromUser = await feedApiClient.GetUser(action.FK_From.ToString());

                userSignature = await feedApiClient.GetUserSignature(action.FK_From.ToString());
            }

            var templateRepository = new TemplateRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            var outgoingMemoRepository = new OutgoingMemoRepository(Context);
            var correspondeceDocumentRepository = new CorrespondenceDocumentsRepository(Context);

            var template = templateRepository.GetById(ModuleConstants.MemoDocumentTemplate);

            var outgoingDocument = documentRepository.GetFromTemplate(template, new CreationInfo());

            documentRepository.InitEntity(outgoingDocument, creationInfo);
            outgoingDocument.Title = action.Subject;

            documentRepository.UpdateDocxPlaceHolders(outgoingDocument, new {
                To = toUser.FullName,
                Subject = action.Subject,
                FromName = fromUser.FullName,
                FromJobTitle = userSignature == null ? fromUser.About : userSignature.Title,
                SignatureImage = userSignature == null ? tempSignatureImage : userSignature.Image,
                OutgoingNumber = outgoingNumber,
                OutgoingDate = actionDate.ToString("dd-MM-yyyy"),
                OrgUnit = fromUser.Organization
            });
            documentRepository.AddTextAfterBookMark(outgoingDocument, ModuleConstants.DocumentStartBookMark, action.Brief);
            documentRepository.DeleteComments(outgoingDocument);
            documentRepository.ConvertToPdf(outgoingDocument);

            if(userSignature != null)
            {
                documentRepository.DigitallySignDocument(userSignature.SingingCertificate, outgoingDocument, fromUser.DisplayName, actionDate, fromUser.Id.ToString());
            }

            var outgoingMemoId = Guid.NewGuid();

            var outgoingMemo = new OutgoingMemo
            {
                Subject = action.Subject,
                Brief = action.Brief,
                CreatedBy = action.FK_From.ToString(),
                CreationDate = actionDate,
                From = action.FK_From.ToString(),
                Id = outgoingMemoId,
                FK_Document = outgoingMemoId,
                Language = DB.Language.AR,
                LastModified = actionDate,
                ThreadId = action.ThreadId,
                Status = OutgoingMemoStatus.New,
                To = action.FK_To.ToString(),
                Confidential = false
            };

            outgoingMemoRepository.Insert(outgoingMemo);
            AddMainDocument(outgoingMemo.Id, outgoingDocument, true);

            result = outgoingMemo.Id;

            IList<DocumentsView> attachements = new List<DocumentsView>();

            if (action.AttachAll)
                attachements = GetAllDocuments(action.FK_IncomingMemoId);
            else
                attachements = GetSelectedDocuments(action.FK_IncomingMemoId, action.SelectedAttachements);

            CopyCorrespondenceAttachements(creationInfo, attachements, outgoingMemo.Id, true);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromOutgoingMemo(outgoingMemo);
                if (outgoingMemo.Confidential.Value)
                    feed.Scope = 0;

                feedBatch.feeds.Add(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);

                userAction.Actor = outgoingMemo.From;

                userAction.FK_Feed = outgoingMemo.Id;
                userAction.Destination = outgoingMemo.To;
                userAction.FK_UserActionType = ModuleConstants.MemoActions.ForwardIncomingMemo;
                userAction.ThreadId = outgoingMemo.ThreadId;
                userAction.Subject = outgoingMemo.Subject;
                userAction.Notes = action.Brief;
                feedBatch.actions.Add(userAction);
            }

            var incomingMemoRepository = new IncomingMemoRepository(Context);

            var incomingMemo = incomingMemoRepository.InitEntity(creationInfo);

            var incomingMemoId = Guid.NewGuid();

            incomingMemo.Id = incomingMemoId;
            incomingMemo.Brief = outgoingMemo.Brief;
            incomingMemo.From = fromUser.Id.ToString();
            incomingMemo.IncomingNumber = incomingNumber;
            incomingMemo.IncomingDate = actionDate;
            incomingMemo.FK_Document = outgoingDocument.Id;
            incomingMemo.Parent = new Correspondence { Id = outgoingMemo.Id };
            incomingMemo.Status = IncomingMemoStatus.New;
            incomingMemo.Subject = outgoingMemo.Subject;
            incomingMemo.To = toUser.Id.ToString();
            incomingMemo.FK_Outgoing = outgoingMemo.Id;
            incomingMemo.ThreadId = action.ThreadId;

            incomingMemoRepository.Insert(incomingMemo);

            AddMainDocument(incomingMemo.Id, outgoingDocument, false);

            CopyCorrespondenceAttachements(creationInfo, attachements, incomingMemo.Id, false);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromIncomingMemo(incomingMemo);
                feed.Status = 1;

                feedBatch.feeds.Add(feed);

                await feedApiClient.InsertBatch(feedBatch);
            }

            try
            {
                var telegramGateway = new TelegramGateway();
                telegramGateway.SendMessage(string.Format("مراسلة جديدة ({0}) من ({1}) التعليق ({2})", outgoingMemo.Subject,fromUser.FullName, action.Brief));
            }
            catch { }

            try
            {
                YammerGateway.SendMessage(string.Format("مراسلة جديدة ({0}) من ({1}) التعليق ({2})", outgoingMemo.Subject, fromUser.FullName, action.Brief));
            }
            catch { }

            return result;
        }
    }
}
