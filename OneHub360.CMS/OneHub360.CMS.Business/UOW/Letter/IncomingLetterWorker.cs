using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using System.IO;
using System.Web.Hosting;
using OneHub360.App.Shared;
using OneHub360.DB;
using System.Web;

namespace OneHub360.CMS.Business
{
    public class IncomingLetterWorker : LetterWorker
    {
        public IncomingLetterWorker(WorkerMode mode) : base(mode)
        {

        }

        public void UpdateLetterStatus(Guid letterId,int status)
        {
            var incomingLetterRepository = new IncomingLetterRepository(Context);
            var incomingLetter = incomingLetterRepository.GetById(letterId);
            incomingLetter.Status = status;
            incomingLetterRepository.Update(incomingLetter);
        }
        public Guid Reject(IncomingLetter incomingLetter)
        {

            var incomingLetterRepository = new IncomingLetterRepository(Context);
           
            incomingLetter.IncomingNumber = string.Format("{0}-{1}", CMSConfigLoader.Generator.configData.NumbersPrefix, NumberGenerator.Generator.LetterIncomingNumber.ToString().PadLeft(5, '0'));
            incomingLetter.IncomingDate = DateTime.Now;
            incomingLetter.RegisteringDate = DateTime.Now;
            incomingLetter.Status = IncomingLetterStatus.Rejected;
            incomingLetter.RegisteredBy = incomingLetter.CreatedBy;
            
            incomingLetter.FK_Document = incomingLetter.Id;
            incomingLetterRepository.Insert(incomingLetter);
           
            SendFromReg(incomingLetter.Id);
            

            return incomingLetter.Id;
        }
        public async Task<Guid> Register(IncomingLetter incomingLetter)
        {
            var incomingLetterRepository = new IncomingLetterRepository(Context);
            incomingLetter.Id = Guid.NewGuid();
            if (!incomingLetter.ThreadId.HasValue)
                incomingLetter.ThreadId = Guid.NewGuid();
            incomingLetter.IncomingNumber = string.Format("{0}-{1}", CMSConfigLoader.Generator.configData.NumbersPrefix, NumberGenerator.Generator.LetterIncomingNumber.ToString().PadLeft(5, '0'));
            incomingLetter.IncomingDate = DateTime.Now;
            incomingLetter.RegisteringDate = DateTime.Now;
            incomingLetter.Status = IncomingLetterStatus.Registered;

            incomingLetter.FK_Document = incomingLetter.Id;

            //var documentRepository = DocumentRepositoryFactory.GetInstance(Context);

            //var template = new TemplateRepository(Context).GetById(ModuleConstants.LetterDocumentTemplate);

            //var document = documentRepository.GetFromTemplate(template,new DB.CreationInfo {
            //    CreatedBy = incomingLetter.CreatedBy,
            //    CreationDate = DateTime.Now,
            //    Id = Guid.NewGuid(),
            //    IsDeleted = false,
            //    Language = DB.Language.AR,
            //    LastModified = DateTime.Now
            //});
            //document.Id = incomingLetter.Id;
            //document.Title = incomingLetter.Subject;
            //document.FileName = Path.ChangeExtension(document.FileName, "pdf");

            //document.FileBinary = File.ReadAllBytes(HostingEnvironment.MapPath(@"~/temp/letter.pdf"));

            //incomingLetter.FK_Document = document.Id;

            incomingLetterRepository.Insert(incomingLetter);

            //AddMainDocument(incomingLetter.Id, document, true);

            //using (var feedApiClient = new FeedApiClient())
            //{
            //    var feed = feedApiClient.GenerateFromIncomingLetter(incomingLetter);
            //    feed.Status = 0;

            //    await feedApiClient.InsertFeed(feed);
            //}

                return incomingLetter.Id;
        }

        public IncomingLetterView GetView(Guid outgoingLetterId)
        {
            IncomingLetterView incomingLetterView;
            using (var context = new CMSContext())
            {
                using (var incomingLetterViewRepository = new IncomingLetterViewRepository(context))
                {
                    incomingLetterView = incomingLetterViewRepository.GetById(outgoingLetterId);
                }

            }
            return incomingLetterView;
        }

        public IList<IncomingLetterView> GetRegisteredLetters()
        {
            long totalCount;
            var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);
            return incomingLetterViewRepository.GetPaged(string.Format("Status = {0}",IncomingLetterStatus.Registered), "CreationDate desc", 0, int.MaxValue, out totalCount);
        }

        public IList<IncomingLetterView> GetLetters(int status)
        {
            long totalCount;
            var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);

            return incomingLetterViewRepository.GetPaged(string.Format("(Status = {0} or {0} = -1)", status), "CreationDate desc", 0, int.MaxValue, out totalCount);
        }
        public IncomingLetterView GetLetter(string id)
        {
            long totalCount;
            var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);

            return incomingLetterViewRepository.GetPaged(string.Format(" (Id = '{0}' )", id), "CreationDate desc", 0, int.MaxValue, out totalCount).FirstOrDefault();
        }
        public virtual int GetPagesCount(string whereClause)
        {
            var number = Context.Session
            .CreateSQLQuery("Select count(*) from IncomingLetterView where" + whereClause)
            .UniqueResult<int>();
            

            //var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);
           return  number;
            
            //totalCount = incomingLetterViewRepository.GetTotalItems(whereClause);
            //return totalCount;
        }
        public virtual IEnumerable<IncomingLetterView> GetPagedTimeLineView(string whereClause, int pagenum, int pagelength)
        {
            var List = Context.Session
            .CreateSQLQuery("Select * from IncomingLetterView where" + whereClause + " order by CreationDate desc")
            .SetFirstResult(pagenum-1*pagelength).SetMaxResults(pagelength).List();
            List<IncomingLetterView> incomingLetterViews = new List<IncomingLetterView>(); 
            foreach (var item in List)
            {

                incomingLetterViews.Add(new IncomingLetterView() {
                    Id = (Guid)((object[])item)[0],
                    CreatedBy = (string)((object[])item)[1],
                    CreationDate = (DateTime)((object[])item)[2],
                    EntityId = (Guid)((object[])item)[6],
                    Subject = (string)((object[])item)[7],
                    From = (string)((object[])item)[8],
                    To = (string)((object[])item)[9],
                    ThreadId = (Guid?)((object[])item)[13],
                    FK_Document = (Guid?)((object[])item)[14],
                    IncomingNumber = (string)((object[])item)[17],
                    IncomingDate = (DateTime?)((object[])item)[18],
                    OutgoingNumber = (string)((object[])item)[19],
                    OutgoingDate = (DateTime?)((object[])item)[20],
                    RegisteringDate = (DateTime)((object[])item)[21],
                    RegisteredBy = (string)((object[])item)[22],
                    Status = (int?)((object[])item)[31]
                });
            }
            //var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);

            //IEnumerable<IncomingLetterView> incomingLetterViews;
            //var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);
            //long totalCount;
            //incomingLetterViews = incomingLetterViewRepository.GetPaged(whereClause, " CreationDate desc", pagelength * (pagenum - 1), pagelength, out totalCount);
            return incomingLetterViews;
        }
        public IList<IncomingLetterView> GetLetters(filteroption filteroption)
        {
            long totalCount;
            var incomingLetterViewRepository = new IncomingLetterViewRepository(Context);

            return incomingLetterViewRepository.GetPaged(string.Format("(Status = {0} or {0} = -1)", filteroption.status), " CreationDate desc", 0, int.MaxValue, out totalCount);
        }

        public async Task<Guid> ForwardIncomingLetter(IncomingLetterAction action)
        {
            var result = Guid.Empty;

            var actionDate = DateTime.Now;
            var fromUser = new UserInfoAutoComplete();
            var toUser = new UserInfoAutoComplete();

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
            }

            var templateRepository = new TemplateRepository(Context);
            var documentRepository = DocumentRepositoryFactory.GetInstance(Context);
            var outgoingMemoRepository = new OutgoingMemoRepository(Context);
            var correspondeceDocumentRepository = new CorrespondenceDocumentsRepository(Context);

            var template = templateRepository.GetById(ModuleConstants.MemoDocumentTemplate);

            var outgoingDocument = documentRepository.GetFromTemplate(template, new CreationInfo());

            documentRepository.InitEntity(outgoingDocument, creationInfo);
            outgoingDocument.Title = action.Subject;

            documentRepository.UpdateDocxPlaceHolders(outgoingDocument, new
            {
                To = toUser.FullName,
                Subject = action.Subject,
                FromName = fromUser.FullName,
                FromJobTitle = fromUser.About,
                SignatureImage = signatureImage,
                OutgoingNumber = outgoingNumber,
                OutgoingDate = actionDate.ToString("dd-MM-yyyy"),
                OrgUnit = fromUser.Organization
            });
            documentRepository.AddTextAfterBookMark(outgoingDocument, ModuleConstants.DocumentStartBookMark, action.Brief);
            documentRepository.DeleteComments(outgoingDocument);
            documentRepository.ConvertToPdf(outgoingDocument);

            //documentRepository.DigitallySignDocument(outgoingDocument, fromUser.FullName, actionDate, fromUser.Id.ToString());

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
                attachements = GetAllDocuments(action.FK_IncomingLetterId);
            else
                attachements = GetSelectedDocuments(action.FK_IncomingLetterId, action.SelectedAttachements);

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

            return result;
        }

        public IncomingLetter GetLetter(Guid incomingLetterId)
        {
            var incomingLetterRepository = new IncomingLetterRepository(Context);
            return incomingLetterRepository.GetById(incomingLetterId);
        }

        public async Task<bool> SendFromReg(Guid incomingLetterId)
        {
            var incomingLetterRepository = new IncomingLetterRepository(Context);
            var incomingLetter = incomingLetterRepository.GetById(incomingLetterId);
            incomingLetter.Status = BaseStatus.Sent;
            incomingLetterRepository.Update(incomingLetter);

            using (var feedApi = new FeedApiClient())
            {
                var incomingLetterFeed = feedApi.GenerateFromIncomingLetter(incomingLetter);
                incomingLetterFeed.Scope = 2;
                incomingLetterFeed.Text3 = incomingLetter.ThreadId.ToString();
                await feedApi.InsertFeed(incomingLetterFeed);

                var action = new UserAction {
                    Actor = incomingLetter.From,
                    CreatedBy = incomingLetter.CreatedBy,
                    CreationDate = DateTime.Now,
                    BrowserType = "",
                    Destination = incomingLetter.To,
                    Notes = "",
                    Subject = incomingLetter.Subject,
                    ThreadId = incomingLetter.ThreadId
                };

                await feedApi.InsertAction(action);
            }

            return true;
        }

        public bool Update(IncomingLetter incomingLetter)
        {
            var incomingLetterRepository = new IncomingLetterRepository(Context);
            incomingLetterRepository.Update(incomingLetter);
            return true;
        }
    }
}
