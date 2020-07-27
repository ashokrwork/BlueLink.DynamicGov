using OneHub360.App.Shared;
using OneHub360.Business;
using OneHub360.CMS.Business.Messages;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Linq;
using OneHub360.DB;
using Newtonsoft.Json;
using OneHub360.App.Shared.General;

namespace OneHub360.CMS.Business
{
    public class DraftMemoWorker : MemoWorker
    {
        public DraftMemoWorker(WorkerMode mode) : base(mode)
        {
        }

        public DraftMemoWorker(WorkerMode mode, bool isStateless) : base(mode)
        {
        }

        public MultiSendView GetMultiSendView(Guid correspondenceId)
        {
            using (var context = new CMSContext(false))
            {
                var multiSendViewRespository = new MultiSendViewRespository(context);

                return multiSendViewRespository.GetById(correspondenceId);
            }
        }

        public bool UpdateMultiSendView(MultiSendView multiSendView)
        {
            var correspondenceRepository = new CorrespondenceRepository(Context);

            var correspondence = correspondenceRepository.GetById(multiSendView.Id);

            correspondence.CopyTo = multiSendView.CopyTo;
            correspondence.AddtionalRecipients = multiSendView.AddtionalRecipients;

            correspondenceRepository.Update(correspondence);

            return true;
        }

        public bool DeleteAttachement(Guid attachementId)
        {
            var result = false;

            var correspondenceDocumentRepository = new CorrespondenceDocumentsRepository(Context);

            var correspondenceDocument = correspondenceDocumentRepository.GetById(attachementId);

            var totalCount = correspondenceDocumentRepository.GetTotalItems(string.Format("FK_Document = '{0}'",correspondenceDocument.FK_Document));

            correspondenceDocumentRepository.ForceDelete(attachementId);

            if (totalCount == 1)
            {
                var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
                documentRepository.Delete(correspondenceDocument.FK_Document);
            }
            result = true;

            return result;
        }

        public async Task<Guid> ProcessCreate(DraftMemo draftMemo)
        {
            var result = Guid.Empty;

            switch (Mode)
            {
                //case WorkerMode.Queue:
                //    var createDraftMemoMessage = new CreateDraftMemoMessage();
                //    createDraftMemoMessage.DraftMemo = draftMemo;
                //    result = QueueWorkMessage(createDraftMemoMessage);
                //    break;
                case WorkerMode.NonQueue:
                    result = await Create(draftMemo);
                    break;
            }
            return result;
        }

        public async Task<Guid> Create(DraftMemo draftMemo)
        {
            

            var userInfo = new UserInfoAutoComplete();

            var batchFeedInsert = new EntitiesBatchInsert();

            using (var feedApiClient = new FeedApiClient())
            {
                userInfo = await feedApiClient.GetUser(draftMemo.To);
            }

            draftMemo.Id = Guid.NewGuid();
            draftMemo.CreationDate = DateTime.Now;
            draftMemo.Status = BaseStatus.New;

            draftMemo.AddtionalRecipients = "[]";
            draftMemo.CopyTo = "[]";

            var creationInfo = new CreationInfo() { CreatedBy = draftMemo.CreatedBy, CreationDate = draftMemo.CreationDate, IsDeleted = draftMemo.IsDeleted, Language = Language.AR, LastModified = draftMemo.LastModified };

            var templateRepository = new TemplateRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            var draftMemoRepository = new DraftMemoRepository(Context);

            var template = templateRepository.GetById(ModuleConstants.MemoDocumentTemplate);

            creationInfo.Id = draftMemo.Id;

            var document = documentRepository.GetFromTemplate(template, creationInfo);
            document.Title = draftMemo.Subject;
            documentRepository.UpdateDocxPlaceHolders(document, 
                new {
                    To = userInfo.FullName, Subject = draftMemo.Subject,
                    OrgUnit = userInfo.Organization
                });
            //documentRepository.AddTextAfterBookMark(document, ModuleConstants.DocumentStartBookMark, draftMemo.Brief);

            draftMemo.FK_Document = document.Id;
            draftMemo.ThreadId = Guid.NewGuid();

            draftMemoRepository.Insert(draftMemo);
            AddMainDocument(draftMemo.Id, document,true);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromDraftMemo(draftMemo);
                if (draftMemo.Confidential.Value)
                    feed.Scope = 0;
                //await feedApiClient.InsertFeed(feed);

                batchFeedInsert.feeds.Add(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);
                userAction.Actor = draftMemo.From;
                userAction.FK_Feed = draftMemo.Id;
                userAction.Destination = draftMemo.To;
                userAction.FK_UserActionType = ModuleConstants.MemoActions.CreateDraftMemo;
                userAction.ThreadId = draftMemo.ThreadId;
                userAction.Subject = draftMemo.Subject;

                //batchFeedInsert.actions.Add(userAction);

                await feedApiClient.InsertBatch(batchFeedInsert);

                //await feedApiClient.InsertAction(userAction);
            }

            return draftMemo.Id;
        }

        public async Task<bool> Send(Guid draftMemoId,Guid userId, string userPin)
        {
            var result = false;

            switch (Mode)
            {
                case (WorkerMode.NonQueue):
                    {
                        var draftMemo = new DraftMemoRepository(Context).GetById(draftMemoId);

                       await SendToSingleUser(draftMemo, userId, draftMemo.To, userPin);
                        if(draftMemo.AddtionalRecipients != "[]")
                        {
                            var addtionalRecipients = JsonConvert.DeserializeObject<string[]>(draftMemo.AddtionalRecipients);
                            foreach(string toUserId in addtionalRecipients)
                            {
                                await SendToSingleUser(draftMemo, userId, toUserId, userPin);
                            }
                        }

                        break;
                    }
            }
            return result;
        }

        public async Task<bool> SendToSingleUser(DraftMemo draftMemo,Guid userId, string toUserId, string userPin)
        {
            

            var tempSignatureImage = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/signature.png"));

            Signature userSignature;
            var fromUser = new UserInfoAutoComplete();
            var toUser = new UserInfoAutoComplete();

            var threadId = Guid.NewGuid();
            string OrganizationUnitPrifix="";
            string OrganizationUnitLastnumber="";
            OrganizationUnitHelper oU = new OrganizationUnitHelper();
            var strings = oU.GetSerialParts(userId.ToString());
            OrganizationUnitPrifix = strings[0];
            OrganizationUnitLastnumber = strings[1];

            var outgoingNumber = string.Format("{0}-{1}-{2}", CMSConfigLoader.Generator.configData.NumbersPrefix,OrganizationUnitPrifix, OrganizationUnitLastnumber.PadLeft(5, '0'));
            var incomingNumber = string.Format("{0}-{1}-{2}", CMSConfigLoader.Generator.configData.NumbersPrefix,OrganizationUnitPrifix, OrganizationUnitLastnumber.PadLeft(5, '0'));


            var actionDate = DateTime.Now;

            var draftDocument = new Document();
            var outgoingDocument = new Document();


            
            OutgoingMemo outgoingMemo;
            IncomingMemo incomingMemo;


            var draftMemoRepository = new DraftMemoRepository(Context);

            

            if (draftMemo.ThreadId.HasValue)
                threadId = draftMemo.ThreadId.Value;

            draftMemo.IncomingDate = actionDate;
            draftMemo.From = userId.ToString();
            draftMemo.OutgoingDate = actionDate;
            draftMemo.IncomingNumber = incomingNumber;
            draftMemo.OutgoingNumber = outgoingNumber;
            draftMemo.ThreadId = threadId;

            draftMemoRepository.Update(draftMemo);

            using (var feedApiClient = new FeedApiClient())
            {
                var newValues = new Dictionary<string, object>();
                newValues.Add("Id", draftMemo.Id);
                newValues.Add("Status", 2);
                newValues.Add("FK_From", userId.ToString());
                await feedApiClient.UpdateFeed(newValues);
            }


            using (var feedApiClient = new FeedApiClient())
            {
                fromUser = await feedApiClient.GetUser(userId.ToString());
                toUser = await feedApiClient.GetUser(toUserId);
                userSignature = await feedApiClient.GetUserSignature(userId.ToString());
            }

            var creationInfo = new CreationInfo
            {
                CreatedBy = fromUser.Id.ToString(),
                CreationDate = actionDate,
                IsDeleted = false,
                Language = Language.AR,
                LastModified = actionDate
            };

            var draftAttachements = GetAttachements(draftMemo.Id);

            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            draftDocument = documentRepository.GetById(draftMemo.FK_Document);
            outgoingDocument = (Document)draftDocument.Clone();
            outgoingDocument.Id = Guid.NewGuid();
            outgoingDocument.Title = draftMemo.Subject;

            documentRepository.UpdateDocxPlaceHolders(outgoingDocument,
                new
                {
                    To = toUser.FullName,
                    FromName = fromUser.FullName,
                    FromJobTitle = userSignature == null ? fromUser.About : userSignature.Title,
                    SignatureImage = userSignature == null ? tempSignatureImage : userSignature.Image,
                    OutgoingNumber = outgoingNumber,
                    OutgoingDate = actionDate.ToString("dd-MM-yyyy"),
                    OrgUnit = fromUser.Organization
                });

            documentRepository.DeleteComments(outgoingDocument);

            documentRepository.ConvertToPdf(outgoingDocument);

            if (userSignature != null)
            {
                documentRepository.DigitallySignDocument(userSignature.SingingCertificate, outgoingDocument, fromUser.FullName, actionDate, fromUser.Id.ToString());
            }

            var outgoingMemoRepository = new OutgoingMemoRepository(Context);

            outgoingMemo = outgoingMemoRepository.InitEntity(creationInfo);
            outgoingMemo.Id = Guid.NewGuid();
            outgoingMemo.Brief = draftMemo.Brief;
            outgoingMemo.FKDraft = draftMemo.Id;
            outgoingMemo.FK_Document = outgoingMemo.Id;
            outgoingMemo.From = fromUser.Id.ToString();
            outgoingMemo.OutgoingDate = actionDate;
            outgoingMemo.OutgoingNumber = outgoingNumber;
            outgoingMemo.Parent = new Correspondence { Id = draftMemo.Id };
            outgoingMemo.Status = OutgoingMemoStatus.Sent;
            outgoingMemo.Subject = draftMemo.Subject;
            outgoingMemo.To = toUser.Id.ToString();
            outgoingMemo.ThreadId = threadId;

            outgoingMemo = outgoingMemoRepository.Insert(outgoingMemo);

            AddMainDocument(outgoingMemo.Id, outgoingDocument, true);

            CopyCorrespondenceAttachements(creationInfo, draftAttachements, outgoingMemo.Id, true);


            var incomingMemoRepository = new IncomingMemoRepository(Context);

            incomingMemo = incomingMemoRepository.InitEntity(creationInfo);

            incomingMemo.Brief = draftMemo.Brief;
            incomingMemo.From = fromUser.Id.ToString();
            incomingMemo.IncomingNumber = incomingNumber;
            incomingMemo.IncomingDate = actionDate;
            incomingMemo.FK_Document = outgoingDocument.Id;
            incomingMemo.Parent = new Correspondence { Id = outgoingMemo.Id };
            incomingMemo.Status = IncomingMemoStatus.New;
            incomingMemo.Subject = draftMemo.Subject;
            incomingMemo.To = toUser.Id.ToString();
            incomingMemo.FK_Draft = draftMemo.Id;
            incomingMemo.FK_Outgoing = outgoingMemo.Id;
            incomingMemo.ThreadId = threadId;

            incomingMemoRepository.Insert(incomingMemo);

            AddMainDocument(incomingMemo.Id, outgoingDocument, false);

            CopyCorrespondenceAttachements(creationInfo, draftAttachements, incomingMemo.Id, false);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromOutgoingMemo(outgoingMemo);
                feed.Status = 2;

                await feedApiClient.InsertFeed(feed);
            }



            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromIncomingMemo(incomingMemo);
                feed.Status = 1;

                await feedApiClient.InsertFeed(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);

                userAction.Actor = draftMemo.From;
                userAction.FK_Feed = draftMemo.Id;
                userAction.CreationDate = actionDate;
                userAction.Destination = toUserId;
                userAction.FK_UserActionType = ModuleConstants.MemoActions.SendDraftMemo;
                userAction.ThreadId = threadId;
                userAction.Subject = draftMemo.Subject;

                await feedApiClient.InsertAction(userAction);
            }

            if(draftMemo.CopyTo != "[]")
            {
                var copyTo = JsonConvert.DeserializeObject<string[]>(draftMemo.CopyTo);
                foreach (string copyToUserId in copyTo)
                {
                    await CopyIncomingMemo(draftMemo, incomingMemo, copyToUserId, creationInfo);
                }
            }
            oU.updateLastNumber(userId.ToString());
            return true;
        }

        public async Task<bool> CopyIncomingMemo(DraftMemo draftMemo, IncomingMemo incomingMemo,string toUserId,CreationInfo creationInfo)
        {
            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromIncomingMemo(incomingMemo);
                feed.Status = 1;

                await feedApiClient.InsertFeed(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);

                userAction.Actor = draftMemo.From;
                userAction.FK_Feed = draftMemo.Id;
                userAction.CreationDate = creationInfo.CreationDate;
                userAction.Destination = toUserId;
                userAction.FK_UserActionType = ModuleConstants.MemoActions.SendDraftMemo;
                userAction.ThreadId = draftMemo.ThreadId;
                userAction.Subject = draftMemo.Subject;
                userAction.Notes = "(نسخة)";

                await feedApiClient.InsertAction(userAction);
            }

            return true;
        }

        public DraftMemoView GetView(Guid draftId)
        {
            DraftMemoView draftMemoView;

            using (var context = new CMSContext(false))
            {
                var draftsViewRepository = new DraftMemoViewRepository(context);

                context.Session.Flush();

                draftMemoView = draftsViewRepository.GetById(draftId);
            }

            return draftMemoView;
        }

        public DraftMemo GetMemo(Guid draftId)
        {
            
            DraftMemo draftMemoView;
            using (var context = new CMSContext(false))
            {
                var draftsRepository = new DraftMemoRepository(context);

                draftMemoView = draftsRepository.GetById(draftId);
            }

            return draftMemoView;
        }

        public async Task<bool> Update(DraftMemo draftMemo)
        {
            var result = true;
            
            var draftsRepository = new DraftMemoRepository(Context);

            draftsRepository.Update(draftMemo);

            var userInfo = new UserInfoAutoComplete();

            var batchFeedInsert = new EntitiesBatchInsert();

            using (var feedApiClient = new FeedApiClient())
            {
                userInfo = await feedApiClient.GetUser(draftMemo.To);
            }

            

            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            long totalCount = 0;
            var document = documentRepository.GetById(GetMainDocument(draftMemo.Id).Id);

            documentRepository.UpdateDocxPlaceHolders(document,
                new
                {
                    To = userInfo.FullName,
                    Subject = draftMemo.Subject,
                    OrgUnit = userInfo.Organization
                });

            documentRepository.Update(document);

            using (var feedApiClient = new FeedApiClient())
            {
                var newValues = new Dictionary<string, object>();
                newValues.Add("Id", draftMemo.Id);
                newValues.Add("FK_To", Guid.Parse(draftMemo.To));
                newValues.Add("FK_From",Guid.Parse(draftMemo.From));
                newValues.Add("Title", draftMemo.Subject);


                await feedApiClient.UpdateFeed(newValues);
            }


                return result;
        }

        public IList<string> GetPrintUrls(Guid draftMemoId)
        {
            long totalCount;
            Guid draftMainDocumentId;

            IList<string> printUrls;

            using (var context = new CMSContext())
            {
                using (var draftMemoViewRepository = new DraftMemoViewRepository(context))
                {
                    var draftMemo = draftMemoViewRepository.GetById(draftMemoId);
                    draftMainDocumentId = draftMemo.FK_Document.Value;
                }

                using (var documentsViewRepository = new DocumentsViewRepository(context))
                {
                    printUrls = documentsViewRepository.GetPaged(string.Format("Id = '{0}' or FK_Correspondence = '{1}'", draftMainDocumentId, draftMemoId, draftMemoId), "CreationDate desc", 0, int.MaxValue, out totalCount).Select(D => D.PrintUrl).ToList();
                }
            }
            return printUrls;
        }
    }
}
