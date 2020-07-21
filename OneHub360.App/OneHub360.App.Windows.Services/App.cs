using OneHub360.App.Business.UOW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Windows.Services
{
    public virtual partial class App : ServiceBase
    {
        public virtual App()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var queueWorker = new QueueWorkerBase();

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
