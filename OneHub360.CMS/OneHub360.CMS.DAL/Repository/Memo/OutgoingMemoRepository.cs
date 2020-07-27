using NHibernate;
using OneHub360.DB;
using System;

namespace OneHub360.CMS.DAL
{
    public class OutgoingMemoRepository : NHEntityRepository<OutgoingMemo>
    {
        public OutgoingMemoRepository(IDBContext context) : base(context)
        {
        }

        

        
    }
}
