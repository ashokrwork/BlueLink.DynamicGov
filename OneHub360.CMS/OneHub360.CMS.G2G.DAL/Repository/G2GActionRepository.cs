using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.G2G.DAL
{
    public class G2GActionRepository : NHEntityRepository<G2GAction>
    {
        public G2GActionRepository(IDBContext context) : base(context)
        {
        }
    }
}
