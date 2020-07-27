using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class DraftMemoViewRepository : NHEntityRepository<DraftMemoView>
    {
        public DraftMemoViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
