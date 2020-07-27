using OneHub360.CMS.Business.UOW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Windows.Services
{
    public partial class CMS : ServiceBase
    {
        public CMS()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var queueWorker = new WorkerBase();

            while (true)
            {
                var message = queueWorker.RecieveWorkMessage();
                message.Process();
            }
        }

        protected override void OnStop()
        {
        }
    }
}
