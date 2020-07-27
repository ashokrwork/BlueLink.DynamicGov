using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class IncomingMemoViewRepository : NHEntityRepository<IncomingMemoView>
    {
        public IncomingMemoViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
