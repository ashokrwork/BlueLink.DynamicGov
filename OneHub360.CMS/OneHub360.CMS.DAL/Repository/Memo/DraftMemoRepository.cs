using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class DraftMemoRepository : NHEntityRepository<DraftMemo>
    {
        public DraftMemoRepository(IDBContext context) : base(context)
        {
        }
    }
}
