using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class SharingViewRepository : NHEntityRepository<SharingView>
    {
        public SharingViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
