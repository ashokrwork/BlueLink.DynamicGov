using OneHub360.WPF.Register.App.Controls.Incoming;
using OneHub360.WPF.Register.App.Controls.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneHub360.WPF.Register.App.Controls
{
    /// <summary>
    /// Interaction logic for MainHome.xaml
    /// </summary>
    public partial class MainHome : UserControl
    {
        public MainHome()
        {
            InitializeComponent();

            using (var client = new APIClient())
            {
                var externalOrganizations = client.GetExternalOrganizations();
                ((App)Application.Current).ExternalOrganizationSource = externalOrganizations;

                var internalOrganizations = client.GetInternalOrganizations();
                ((App)Application.Current).InternalUsersSource = internalOrganizations;
            }
        }

        private void IncomingLetterClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowControl(new IncomingLettersList());
        }

        private void OutgoingLetterClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowControl(new OutgoingLetters());

        }
    }
}
