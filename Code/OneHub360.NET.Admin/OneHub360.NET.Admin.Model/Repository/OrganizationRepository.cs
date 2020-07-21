using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    public class OrganizationRepository : NHEntityRepository<Organization>
    {
        public virtual Organization GetLocal()
        {
            var entity = this.Session.QueryOver<Organization>().Where(x => x.IsLocal == true).SingleOrDefault();
            return entity;

        }
    }
}
