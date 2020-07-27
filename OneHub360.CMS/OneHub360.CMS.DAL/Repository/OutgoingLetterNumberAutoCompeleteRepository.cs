using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public class OutgoingLetterNumberAutoCompeleteRepository : NHEntityRepository<OutgoingLetterNumberAutoCompelete>
    {
        public OutgoingLetterNumberAutoCompeleteRepository(IDBContext context) : base(context)
        {
        }
    }
}
