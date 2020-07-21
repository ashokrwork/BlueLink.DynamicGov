using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class FeedRepository : NHEntityRepository<Feeds>
    {
        public FeedRepository(IDBContext context) : base(context)
        {
        }

        
    }
}
