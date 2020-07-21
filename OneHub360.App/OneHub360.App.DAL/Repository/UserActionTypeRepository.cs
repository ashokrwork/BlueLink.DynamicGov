using OneHub360.App.Shared;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.DAL
{
    public class UserActionTypeRepository : NHEntityRepository<UserActionType>
    {
        public UserActionTypeRepository(IDBContext context) : base(context)
        {
        }
    }
}
