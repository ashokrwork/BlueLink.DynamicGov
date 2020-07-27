using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.Repository.MOI
{
    class ReportPartyRepository : NHEntityRepository<ReportParty>
    {
        public ReportPartyRepository(IDBContext context) : base(context)
        {
        }
    }
}
