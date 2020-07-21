using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class UserInfoAutoCompleteRepository : AdminNHEntityRepository<UserInfoAutoComplete>
    {
        public UserInfoAutoCompleteRepository(IDBContext context) : base(context)
        {
        }
    }
}
