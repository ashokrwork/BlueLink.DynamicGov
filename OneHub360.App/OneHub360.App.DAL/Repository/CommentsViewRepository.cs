using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class CommentsViewRepository : NHEntityRepository<CommentsView>
    {
        public CommentsViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
