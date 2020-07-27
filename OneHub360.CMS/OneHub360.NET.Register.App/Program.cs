using OneHub360.Register.App;
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace OneHub360.Register.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //CultureInfo ci = new CultureInfo("ar");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;

            mainForm = new MetroMain();
            
            // Thread
            //InitializeMonitoringThreads();

            Application.Run(mainForm);
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //monitoringThread.Abort();
        }

        static MetroMain mainForm;
        
        private static void InitializeMonitoringThreads()
        {
            
        }
    }
}
