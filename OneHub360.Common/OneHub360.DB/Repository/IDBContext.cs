using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.DB
{
    public interface IDBContext : IDisposable
    {
        ISessionFactory SessionFactory { get;  }
        ISession Session { get;  }
        ITransaction Transaction { get;  }
    }

   
}
