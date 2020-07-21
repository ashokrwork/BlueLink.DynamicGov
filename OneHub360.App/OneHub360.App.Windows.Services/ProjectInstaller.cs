using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace OneHub360.App.Windows.Services
{
    [RunInstaller(true)]
    public virtual partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public virtual ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
