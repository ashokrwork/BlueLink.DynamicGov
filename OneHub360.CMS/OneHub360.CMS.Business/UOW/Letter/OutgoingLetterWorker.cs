using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.Business;
using OneHub360.CMS.DAL;

namespace OneHub360.CMS.Business
{
    public class OutgoingLetterWorker : LetterWorker
    {
        public OutgoingLetterWorker(WorkerMode mode) : base(mode)
        {
        }

        public OutgoingLetterView GetView(Guid outgoingLetterId)
        {
            OutgoingLetterView outgoingLetterView;
            using (var context = new CMSContext())
            {
                using (var outgoingViewRepository = new OutgoingLetterViewRepository(context))
                {
                    outgoingLetterView = outgoingViewRepository.GetById(outgoingLetterId);
                }

            }
            return outgoingLetterView;
        }

        public IList<OutgoingLetterView> GetLetters(int status)
        {
            long totalCount;
            var outgoingLetterViewRepository = new OutgoingLetterViewRepository(Context);

            return outgoingLetterViewRepository.GetPaged(string.Format("(Status = {0} or {0} = -1)", status), "CreationDate desc", 0, int.MaxValue, out totalCount);
        }

        public IList<OutgoingLetterView> GetAll()
        {
            IList<OutgoingLetterView> outgoingLetterView = new List<OutgoingLetterView>();
            using (var context = new CMSContext())
            {
                using (var outgoingViewRepository = new OutgoingLetterViewRepository(context))
                {
                    outgoingLetterView = outgoingViewRepository.GetAll();
                }

            }
            return outgoingLetterView;
        }

        public OutgoingLetter GetLetter(Guid id)
        {
            using (var context = new CMSContext())
            {
                using (var outgoingLetterRepository = new OutgoingLetterRepository(context))
                {
                    return outgoingLetterRepository.GetById(id);
                }

            }
        }
        public void UpdateLetterStatus(Guid letterId, int status)
        {
            var OutgoingLetterRepository = new OutgoingLetterRepository(Context);
            var OutgoingLetter = OutgoingLetterRepository.GetById(letterId);
            OutgoingLetter.Status = status;
            OutgoingLetterRepository.Update(OutgoingLetter);
          
        }
        public virtual int GetPagesCount(string whereClause)
        {
            var number = Context.Session
            .CreateSQLQuery("Select count(*) from OutgoingLetterView where" + whereClause)
            .UniqueResult<int>();
            return number;

        }
        public virtual IEnumerable<OutgoingLetterView> GetPagedTimeLineView(string whereClause, int pagenum, int pagelength)
        {
            var List = Context.Session
            .CreateSQLQuery("Select * from OutgoingLetterView where" + whereClause + " order by CreationDate desc")
            .SetFirstResult(pagenum - 1 * pagelength).SetMaxResults(pagelength).List();
            List<OutgoingLetterView> OutgoingLetterViews = new List<OutgoingLetterView>();
            foreach (var item in List)
            {

                OutgoingLetterViews.Add(new OutgoingLetterView()
                {
                    Id = (Guid)((object[])item)[0],
                    CreatedBy = (string)((object[])item)[1],
                    CreationDate = (DateTime)((object[])item)[2],
                    EntityId = (Guid)((object[])item)[6],
                    Subject = (string)((object[])item)[7],
                    From = (string)((object[])item)[8],
                    To = (string)((object[])item)[9],
                    ThreadId = (Guid?)((object[])item)[13],
                    FK_Document = (Guid?)((object[])item)[14],
                    CorrespondenceType = (int?)((object[])item)[15],
                    IncomingNumber = (string)((object[])item)[18],
                    IncomingDate = (DateTime?)((object[])item)[19],
                    OutgoingNumber = (string)((object[])item)[20],
                    OutgoingDate = (DateTime?)((object[])item)[21],
                    Status = (int)((object[])item)[29]
                });
            }
            
            return OutgoingLetterViews;
        }
        //public virtual long GetPagesCount(string whereClause)
        //{
        //    var OutgoingLetterViewRepository = new OutgoingLetterRepository(Context);
        //    long totalCount;
        //    totalCount = OutgoingLetterViewRepository.GetTotalItems(whereClause);
        //    return totalCount;
        //}
        //public virtual IEnumerable<OutgoingLetterView> GetPagedTimeLineView(string whereClause, int pagenum, int pagelength)
        //{
        //    IEnumerable<OutgoingLetterView> OutgoingLetterViews;
        //    var OutgoingLetterViewRepository = new OutgoingLetterViewRepository(Context);
        //    long totalCount;
        //    OutgoingLetterViews = OutgoingLetterViewRepository.GetPaged(whereClause, " CreationDate desc", pagelength * (pagenum - 1), pagelength, out totalCount);
        //    return OutgoingLetterViews;
        //}
        public IList<OutgoingLetterView> GetSigned()
        {
            long totalCount;
            IList<OutgoingLetterView> outgoingLetterView = new List<OutgoingLetterView>();
            using (var context = new CMSContext())
            {
                using (var outgoingViewRepository = new OutgoingLetterViewRepository(context))
                {
                    outgoingLetterView = outgoingViewRepository.GetPaged(string.Format("Status = {0}",(int)OutgoingLetterStatus.Signed), string.Empty, 0, 10, out totalCount);
                }

            }
            return outgoingLetterView;
        }

        public void ExportManually(Guid id)
        {
            using (var context = new CMSContext())
            {
                using (var outgoingLetterRepository = new OutgoingLetterRepository(context))
                {
                    var letter = outgoingLetterRepository.GetById(id);
                    letter.Status = OutgoingLetterStatus.SentManual;

                    outgoingLetterRepository.Update(letter);

                    var smsGateway = new SMSGateway();
                    var msgText = string.Format("تم تصدير الكتاب ({0}) يدويا", letter.Subject);
                    smsGateway.SendUnicodeSMS(msgText, ModuleConstants.Demo.SMSAlertNumber);

                    var telegramGateway = new TelegramGateway();
                    telegramGateway.SendMessage(string.Format("تم تصدير الكتاب ({0}) يدويا", letter.Subject));

                    YammerGateway.SendMessage(string.Format("تم تصدير الكتاب ({0}) يدويا", letter.Subject));
                }

                

            }
        }

        public void ExportG2G(Guid id)
        {
            using (var context = new CMSContext())
            {
                using (var outgoingLetterRepository = new OutgoingLetterRepository(context))
                {
                    var letter = outgoingLetterRepository.GetById(id);
                    letter.Status = OutgoingLetterStatus.SentManual;

                    outgoingLetterRepository.Update(letter);

                    var smsGateway = new SMSGateway();
                    var msgText = string.Format("تم تصدير الكتاب ({0}) علي نظام G2G", letter.Subject);
                    smsGateway.SendUnicodeSMS(msgText, ModuleConstants.Demo.SMSAlertNumber);

                    var telegramGateway = new TelegramGateway();
                    telegramGateway.SendMessage(string.Format("تم تصدير الكتاب ({0}) علي نظام G2G", letter.Subject));

                    YammerGateway.SendMessage(string.Format("تم تصدير الكتاب ({0}) علي نظام G2G", letter.Subject));
                }

            }
        }

        public IList<OutgoingLetterNumberAutoCompelete> GetOutgoingNumbers()
        {
            IList<OutgoingLetterNumberAutoCompelete> result = new List<OutgoingLetterNumberAutoCompelete>();
            using (var context = new CMSContext())
            {
                var repository = new OutgoingLetterNumberAutoCompeleteRepository(context);

                result = repository.GetAll();
            }

            return result;
        }
    }
}
