using NHibernate;
using NHibernate.Cfg;
using OneHub360.Business;
using OneHub360.CMS.DAL;
using OneHub360.DB;
using System;
using System.Collections.Generic;

namespace OneHub360.CMS.Business
{
    public class CMSWorkerBase : WorkerBase,IDisposable
    {
        public CMSWorkerBase(WorkerMode mode) : base(mode)
        {
            Context = new CMSContext(true);
        }

       

        protected override string LOCAL_ACK_QUEUE
        {
            get
            {
                return ".\\Private$\\cmsakc";
            }
        }

        protected override string LOCAL_QUEUE
        {
            get
            {
                return ".\\Private$\\cms";
            }
        }

        public void Dispose()
        {
            FinishWork();
            Context.Dispose();
        }
    }
}
