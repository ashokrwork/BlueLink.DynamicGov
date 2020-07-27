using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OneHub360.WPF.Register.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
         public IList<CMS.DAL.ExternalOrganizations> ExternalOrganizationSource { get; set; }
        public IList<OneHub360.App.Shared.UserInfoAutoComplete> InternalUsersSource { get; set; }
    }
}
