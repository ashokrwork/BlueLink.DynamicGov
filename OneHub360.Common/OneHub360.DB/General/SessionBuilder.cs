using NHibernate;
using NHibernate.Cfg;

namespace OneHub360.DB
{
    public class SessionBuilder
    {
        private static ISessionFactory SessionFactory;

        public static ISessionFactory GetInstance()
        {
            if (SessionFactory == null)
            {
                SessionFactory = new Configuration().Configure().BuildSessionFactory();
            }
            return SessionFactory;
        }
    }
}
