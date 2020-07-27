using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHub360.Business;
using OneHub360.CMS.DAL;

namespace OneHub360.CMS.Business
{
    public class LetterWorker : CorrespondeceWorker
    {
        public LetterWorker(WorkerMode mode) : base(mode)
        {
        }

        public IList<ExternalOrganizations> GetExternalOrganizations()
        {
            return new ExternalOrganizationsRepository(Context).GetAll();
        }

        public ExternalOrganizations GetExternalOrganization(Guid id)
        {
            return new ExternalOrganizationsRepository(Context).GetById(id);
        }
    }
}
