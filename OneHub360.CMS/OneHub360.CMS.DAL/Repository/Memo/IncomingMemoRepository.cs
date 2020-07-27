using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class IncomingMemoRepository : NHEntityRepository<IncomingMemo>
    {
        public IncomingMemoRepository(IDBContext context) : base(context)
        {
        }
    }
}
