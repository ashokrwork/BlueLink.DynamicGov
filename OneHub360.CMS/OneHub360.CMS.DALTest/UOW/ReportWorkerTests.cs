using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.Business.UOW;
using OneHub360.CMS.DAL;
using OneHub360.CMS.DAL.Repository.MOI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.UOW.Tests
{
    [TestClass()]
    public class ReportWorkerTests
    {
        [TestMethod()]
        public void CreateReportTest()
        {
            using (var context = new CMSContext(true))
            {
                var reportRepository = new ReportRepository(context);

                var report = new Report()
                {
                    CaseNumber = "12",
                    CreatedBy = "Ahmed Shokr",
                    CaseYear = "2020",
                    CreationDate = DateTime.Now,
                    IncidentDate = DateTime.Now,
                    Language = DB.Language.AR,
                    RecordingDate = DateTime.Now,
                    ReportArea = "Hawalli",
                    ReportDate = DateTime.Now,
                    ReportDescription = "Description",
                    ReportGovernance = Guid.NewGuid(),
                    ReportOfficier = Guid.NewGuid(),
                    ReportSubject = "Subject",
                    Investigator = Guid.NewGuid(),
                    ReportStatus = Guid.NewGuid(),
                    ReportType = Guid.NewGuid()
                
                    
                };

                var result = reportRepository.Insert(report);

                context.Transaction.Commit();
            }
        }
    }
}