using NHibernate;
using NHibernate.Cfg;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace OneHub360.App.Shared
{
    public class OneHubContext : IDBContext
    {
        public OneHubContext()
        {
            InitContext(false);
        }

        public OneHubContext(bool isTransactional)
        {
            InitContext(isTransactional);
        }

        private void InitContext(bool isTransactional)
        {
            var configFilePath = string.Empty;

            if (HostingEnvironment.ApplicationHost == null)
                configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\app\\app.config");
            else
                configFilePath = HostingEnvironment.MapPath("~/config/app/app.config");

            sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();
            session = sessionFactory.OpenSession();
            if (isTransactional)
                transaction = session.BeginTransaction();
        }

        private ISession session;
        private ISessionFactory sessionFactory;
        private ITransaction transaction;

        public ISession Session
        {
            get
            {
                return session;
            }

        }

        public ISessionFactory SessionFactory
        {
            get
            {
                return SessionFactory;
            }


        }

        public ITransaction Transaction
        {
            get
            {
                return transaction;
            }


        }

        public IStatelessSession StalelessSession
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {

            if (session.IsOpen)
                session.Close();

            if (!sessionFactory.IsClosed)
                sessionFactory.Close();

            if (transaction != null)
                transaction.Dispose();

            session.Dispose();

            sessionFactory.Dispose();
        }
    }
}
