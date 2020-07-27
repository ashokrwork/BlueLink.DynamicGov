using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class DocumentsViewRepository : NHEntityRepository<DocumentsView>
    {
        public DocumentsViewRepository(IDBContext context) : base(context)
        {
        }
    }
}
