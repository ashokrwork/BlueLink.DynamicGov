using NHibernate;
using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class UserActionTypeRepository : NHEntityRepository<UserActionType>
    {
        public UserActionTypeRepository(IDBContext context) : base(context)
        {
        }
    }
}
