using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class UserActionRepository : NHEntityRepository<UserAction>
    {
        public UserActionRepository(IDBContext context) : base(context)
        {
        }

        
    }
}
