using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class DraftLetterRepository : NHEntityRepository<DraftLetter>
    {
        public DraftLetterRepository(IDBContext context) : base(context)
        {
        }
    }
}
