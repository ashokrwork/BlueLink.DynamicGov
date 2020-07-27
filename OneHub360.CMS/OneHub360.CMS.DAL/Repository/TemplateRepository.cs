using NHibernate;
using OneHub360.DB;

namespace OneHub360.CMS.DAL
{
    public class TemplateRepository : NHEntityRepository<Template>
    {
        public TemplateRepository(IDBContext context) : base(context)
        {
        }
    }
}
