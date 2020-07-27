using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class OutgoingMemoViewRepository : NHEntityRepository<OutgoingMemoView>
    {
        public OutgoingMemoViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
