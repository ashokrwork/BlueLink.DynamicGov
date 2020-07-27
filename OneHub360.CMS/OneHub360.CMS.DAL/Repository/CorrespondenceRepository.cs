using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class CorrespondenceRepository : NHEntityRepository<Correspondence>
    {
        public CorrespondenceRepository(IDBContext context) : base(context)
        {
        }
    }
}
