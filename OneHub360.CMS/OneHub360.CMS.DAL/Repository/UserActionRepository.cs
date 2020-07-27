using NHibernate;
using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class UserActionRepository : NHEntityRepository<UserAction>
    {
        public UserActionRepository(IDBContext context) : base(context)
        {
        }
    }
}
