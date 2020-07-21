using OneHub360.App.Shared;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.DAL
{
    public class SignatureRepository : AdminNHEntityRepository<Signature>
    {
        public SignatureRepository(IDBContext context) : base(context)
        {
        }
    }
}
