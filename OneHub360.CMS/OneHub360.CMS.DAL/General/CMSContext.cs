using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using System.Web.Hosting;
using System.IO;

namespace OneHub360.CMS.DAL
{
    public class CMSContext : IDBContext
    {
        public CMSContext()
        {
            InitContext(false);
        }

        private void InitContext(bool isTransactional)
        {
            IsTransactional = isTransactional;
            var configFilePath = string.Empty;

            if (HostingEnvironment.ApplicationHost == null)
                configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\modules\\cms\\cms.config");
            else
                configFilePath = HostingEnvironment.MapPath("~/config/modules/cms/cms.config");

            sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();

            session = sessionFactory.OpenSession();
            if (isTransactional)
            
                transaction = session.BeginTransaction();

        }

        public CMSContext(bool isTransactional)
        {
            InitContext(isTransactional);
        }

        private ISession session;
        private IStatelessSession statelessSession;
        private ISessionFactory sessionFactory;
        private ITransaction transaction;
        private bool IsTransactional;

        public ISession Session
        {
            get
            {
                return session;
            }
            
        }

        public IStatelessSession StatelessSession
        {
            get
            {
                return statelessSession;
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
                return statelessSession;
            }
        }

        public void Dispose()
        {
            session.Clear();
            session.Flush();

            if (session.IsOpen)
                session.Close();
            if (transaction != null)
                transaction.Dispose();

            if (!sessionFactory.IsClosed)
                sessionFactory.Close();

            session.Dispose();

            sessionFactory.Dispose();
           
        }
    }

    
}
