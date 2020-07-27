using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class MultiSendViewRespository : NHEntityRepository<MultiSendView>
    {
        public MultiSendViewRespository(IDBContext context) : base(context)
        {
        }
    }
}
