using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class UserActionViewRepository : NHEntityRepository<UserActionView>
    {
        public UserActionViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
