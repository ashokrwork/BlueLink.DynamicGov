using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class UserActionViewRepository : NHEntityRepository<UserActionView>
    {
        public UserActionViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
