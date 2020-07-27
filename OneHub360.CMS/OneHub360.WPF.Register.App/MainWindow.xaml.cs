using OneHub360.WPF.Register.App.Controls;
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
using ToastNotifications;

namespace OneHub360.WPF.Register.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        

        public MainWindow()
        {
            InitializeComponent();
            ShowControl(new MainHome());
        }

        public void ShowControl(Control control)
        {
            control.HorizontalAlignment = HorizontalAlignment.Stretch;
            control.VerticalAlignment = VerticalAlignment.Stretch;
            MainContainer.Children.Clear();
            MainContainer.Children.Add(control);
            mainApplicationMenu.IsOpen = false;
        }

        private void btnMenuIncomingLetters_Click(object sender, RoutedEventArgs e)
        {
            ShowControl(new IncomingLettersList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowControl(new RegisterNewLetter());
            
        }

        public void ShowInformation(string message)
        {
            notifyToast.NotificationsSource = null;
            notifyToast.NotificationsSource = new NotificationsSource();
            notifyToast.NotificationsSource.Show(message, NotificationType.Information);
        }

        private void btnOutgoingList_Click(object sender, RoutedEventArgs e)
        {
            ShowInformation("Test");
        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            ShowControl(new MainHome());
        }
    }
}
