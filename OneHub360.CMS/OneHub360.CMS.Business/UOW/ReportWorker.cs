using OneHub360.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.DAL.Repository.MOI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.UOW
{
    public class ReportWorker : CMSWorkerBase
    {
        public ReportWorker(WorkerMode mode) : base(mode)
        {
        }

        public Guid CreateReport(Report report)
        {
            var reportRepository = new ReportRepository(Context);
            return reportRepository.Insert(report).Id;
        }
    }
}
