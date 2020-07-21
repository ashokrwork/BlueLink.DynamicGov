using OneHub360.App.Shared;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.DAL
{
    public class UserInfoRepository : AdminNHEntityRepository<UserInfos>
    {
        public UserInfoRepository(IDBContext context) : base(context)
        {

        }

        
    }
}
