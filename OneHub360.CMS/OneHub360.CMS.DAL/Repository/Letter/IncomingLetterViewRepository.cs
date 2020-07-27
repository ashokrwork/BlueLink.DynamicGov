using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class IncomingLetterViewRepository : NHEntityRepository<IncomingLetterView>
    {
        public IncomingLetterViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
