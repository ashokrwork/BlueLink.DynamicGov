using OneHub360.App.Shared;
using OneHub360.Business;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Business
{
    public  class AppWorkerBase : WorkerBase, IDisposable
    {
        protected  override string LOCAL_ACK_QUEUE
        {
            get
            {
                return ".\\Private$\\appakc";
            }
        }

        protected override string LOCAL_QUEUE
        {
            get
            {
                return ".\\Private$\\app";
            }
        }

        

        public AppWorkerBase(WorkerMode mode) : base(mode)
        {
            Context = new OneHubContext(true);
        }

        public void Dispose()
        {
            FinishWork();
            Context.Dispose();
        }
    }
}
