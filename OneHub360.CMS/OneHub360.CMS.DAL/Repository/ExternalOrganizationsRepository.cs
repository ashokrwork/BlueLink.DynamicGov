using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class ExternalOrganizationsRepository : NHEntityRepository<ExternalOrganizations>
    {
        public ExternalOrganizationsRepository(IDBContext context) : base(context)
        {
        }

        
    }
}
