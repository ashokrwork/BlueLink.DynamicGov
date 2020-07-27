using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using OneHub360.DB;
using OneHub360.App.Shared;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace OneHub360.CMS.Business
{
    public class DraftLetterWorker : LetterWorker
    {
        public DraftLetterWorker(WorkerMode mode) : base(mode)
        {
        }

        public async Task<Guid> Create(DraftLetter draftLetter)
        {
           

            var creationInfo = new CreationInfo() { CreatedBy = draftLetter.CreatedBy, CreationDate = draftLetter.CreationDate, IsDeleted = draftLetter.IsDeleted, Language = Language.AR, LastModified = draftLetter.LastModified };

            var externalOrganizationsRepository = new ExternalOrganizationsRepository(Context);
            //var externalOrganization = externalOrganizationsRepository.GetById(Guid.Parse(draftLetter.To));

            draftLetter.Id = Guid.NewGuid();
            draftLetter.CreationDate = DateTime.Now;

            var templateRepository = new TemplateRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            var draftLetterRepository = new DraftLetterRepository(Context);

            var template = templateRepository.GetById(ModuleConstants.LetterDocumentTemplate);

            var document = documentRepository.GetFromTemplate(template, creationInfo);
            document.Id = draftLetter.Id;
            document.Title = draftLetter.Subject;
            documentRepository.UpdateDocxPlaceHolders(document,
                new
                {
                    To = draftLetter.PersonTitle,
                    Subject = draftLetter.Subject
                });

            //documentRepository.AddTextAfterBookMark(document, ModuleConstants.DocumentStartBookMark, draftLetter.Brief);

            draftLetter.FK_Document = document.Id;
            if (!draftLetter.ThreadId.HasValue)
                draftLetter.ThreadId = Guid.NewGuid();

            draftLetter.Status = DraftLetterStatus.New;

            draftLetterRepository.Insert(draftLetter);
            AddMainDocument(draftLetter.Id, document, true);

            using (var feedApiClient = new FeedApiClient())
            {
                var feed = feedApiClient.GenerateFromDraftLetter(draftLetter);
                if (draftLetter.Confidential.Value)
                    feed.Scope = 0;
                
                var batchFeedInsert = new EntitiesBatchInsert();

                batchFeedInsert.feeds.Add(feed);

                var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);
                userAction.Actor = draftLetter.From;
                userAction.FK_Feed = draftLetter.Id;
                userAction.Destination = draftLetter.To;
                userAction.FK_UserActionType = ModuleConstants.LetterActions.CreateDraftLetter;
                userAction.ThreadId = draftLetter.ThreadId;
                userAction.Subject = draftLetter.Subject;

                batchFeedInsert.actions.Add(userAction);

                await feedApiClient.InsertBatch(batchFeedInsert);
            }

            return draftLetter.Id;
        }

        public async Task<bool> Send(Guid draftLetterId, Guid userId, string userPin)
        {
            var result = false;
            Signature userSignature;
            var tempSignatureImage = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/signature.png"));

            switch (Mode)
            {
                case (WorkerMode.NonQueue):
                    
                    var signatureImage = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/signature.png"));
                    var fromUser = new UserInfoAutoComplete();
                    //var toOrganization = new ExternalOrganizations();

                    var threadId = Guid.NewGuid();

                    var outgoingNumber = string.Format("{0}-{1}", CMSConfigLoader.Generator.configData.NumbersPrefix, NumberGenerator.Generator.LetterOutgoingNumber.ToString().PadLeft(5, '0'));
                    
                    var actionDate = DateTime.Now;

                    var draftDocument = new Document();
                    var outgoingDocument = new Document();


                    DraftLetter draftLetter;
                    OutgoingLetter outgoingLetter;
                    


                    var draftLetterRepository = new DraftLetterRepository(Context);

                    draftLetter = draftLetterRepository.GetById(draftLetterId);

                    if (draftLetter.ThreadId.HasValue)
                        threadId = draftLetter.ThreadId.Value;

                    draftLetter.IncomingDate = actionDate;
                    draftLetter.From = userId.ToString();
                    draftLetter.OutgoingDate = actionDate;
                    
                    draftLetter.OutgoingNumber = outgoingNumber;
                    draftLetter.ThreadId = threadId;

                    draftLetterRepository.Update(draftLetter);

                    using (var feedApiClient = new FeedApiClient())
                    {
                        var newValues = new Dictionary<string, object>();
                        newValues.Add("Id", draftLetterId);
                        newValues.Add("Status", 2);
                        newValues.Add("FK_From", userId.ToString());
                        await feedApiClient.UpdateFeed(newValues);
                    }


                    using (var feedApiClient = new FeedApiClient())
                    {
                        fromUser = await feedApiClient.GetUser(userId.ToString());
                        userSignature = await feedApiClient.GetUserSignature(userId.ToString());

                    }

                    //toOrganization = new ExternalOrganizationsRepository(Context).GetById(Guid.Parse(draftLetter.To));

                    var creationInfo = new CreationInfo
                    {
                        CreatedBy = fromUser.Id.ToString(),
                        CreationDate = actionDate,
                        IsDeleted = false,
                        Language = Language.AR,
                        LastModified = actionDate
                    };

                    var draftAttachements = GetAttachements(draftLetter.Id);

                    var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
                    draftDocument = documentRepository.GetById(draftLetter.FK_Document);
                    outgoingDocument = (Document)draftDocument.Clone();
                    outgoingDocument.Id = Guid.NewGuid();
                    outgoingDocument.Title = draftLetter.Subject;

                    documentRepository.UpdateDocxPlaceHolders(outgoingDocument,
                        new
                        {
                            FromName = fromUser.FullName,
                            FromJobTitle = fromUser.About,
                            SignatureImage = userSignature == null ? tempSignatureImage : userSignature.Image,
                            OutgoingNumber = outgoingNumber,
                            OutgoingDate = actionDate.ToString("dd-MM-yyyy")
                        });

                    documentRepository.DeleteComments(outgoingDocument);

                    documentRepository.ConvertToPdf(outgoingDocument);

                    if (userSignature != null)
                    {
                        documentRepository.DigitallySignDocument(userSignature.SingingCertificate, outgoingDocument, fromUser.FullName, actionDate, fromUser.Id.ToString());
                    }


                    var outgoingLetterRepository = new OutgoingLetterRepository(Context);

                    outgoingLetter = outgoingLetterRepository.InitEntity(creationInfo);
                    outgoingLetter.Id = Guid.NewGuid();
                    outgoingLetter.Brief = draftLetter.Brief;
                    outgoingLetter.FK_Document = outgoingLetter.Id;
                    outgoingLetter.From = fromUser.Id.ToString();
                    outgoingLetter.OutgoingDate = actionDate;
                    outgoingLetter.OutgoingNumber = outgoingNumber;
                    outgoingLetter.Parent = new Correspondence { Id = draftLetter.Id };
                    outgoingLetter.Status = OutgoingLetterStatus.New;
                    outgoingLetter.Subject = draftLetter.Subject;
                    outgoingLetter.To = draftLetter.To.ToString();
                    outgoingLetter.ThreadId = threadId;

                    outgoingLetter = outgoingLetterRepository.Insert(outgoingLetter);

                    AddMainDocument(outgoingLetter.Id, outgoingDocument, true);

                    CopyCorrespondenceAttachements(creationInfo, draftAttachements, outgoingLetter.Id, true);


                    

                    using (var feedApiClient = new FeedApiClient())
                    {
                        var feed = feedApiClient.GenerateFromOutgoingLetter(outgoingLetter);
                        feed.Status = 2;

                        await feedApiClient.InsertFeed(feed);

                        var userAction = feedApiClient.InitAction(HttpContext.Current.Request, creationInfo);

                        userAction.Actor = draftLetter.From;
                        userAction.FK_Feed = draftLetter.Id;
                        userAction.CreationDate = actionDate;
                        userAction.Destination = draftLetter.To;
                        userAction.FK_UserActionType = ModuleConstants.LetterActions.SendDraftLetter;
                        userAction.ThreadId = threadId;
                        userAction.Subject = draftLetter.Subject;

                        await feedApiClient.InsertAction(userAction);
                    }
                    break;
            }
            
            return result;
        }

        public DraftLetterView GetView(Guid draftId)
        {
            DraftLetterView draftLetterView;

            var draftsViewRepository = new DraftLetterViewRepository(Context);

            draftLetterView = draftsViewRepository.GetById(draftId);


            return draftLetterView;
        }
        public DraftLetter GetLetter(Guid LetterId)
        {

            DraftLetter draftLetterView;
            using (var context = new CMSContext(false))
            {
                var draftsRepository = new DraftLetterRepository(context);

                draftLetterView = draftsRepository.GetById(LetterId);
            }

            return draftLetterView;
        }
        public async Task<bool> Update(DraftLetter draftLetter)
        {
            var result = true;

            var draftsRepository = new DraftLetterRepository(Context);

            draftsRepository.Update(draftLetter);


            var batchFeedInsert = new EntitiesBatchInsert();

        

            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            long totalCount = 0;
            var document = documentRepository.GetById(GetMainDocument(draftLetter.Id).Id);

            documentRepository.UpdateDocxPlaceHolders(document,
                new
                {
                    To = draftLetter.PersonTitle,
                    Subject = draftLetter.Subject
                });

            documentRepository.Update(document);

            using (var feedApiClient = new FeedApiClient())
            {
                var newValues = new Dictionary<string, object>();
                newValues.Add("Id", draftLetter.Id);
                newValues.Add("FK_To", Guid.Parse(draftLetter.To));
                newValues.Add("FK_From", Guid.Parse(draftLetter.From));
                newValues.Add("Title", draftLetter.Subject);


                await feedApiClient.UpdateFeed(newValues);
            }


            return result;
        }
    }
}
