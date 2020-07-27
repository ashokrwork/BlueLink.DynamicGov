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

namespace OneHub360.WPF.Register.App.Controls.Outgoing
{
    /// <summary>
    /// Interaction logic for OutgoingLetters.xaml
    /// </summary>
    public partial class OutgoingLetters : UserControl
    {
        public OutgoingLetters()
        {
            InitializeComponent();
            LoadLetters();
        }

        private async void LoadLetters()
        {
            using (var client = new APIClient())
            {
                var registeredLetters = await client.GetAll();
                listIncomingLetters.ItemsSource = registeredLetters;
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var letterId = Guid.Parse(((Button)e.Source).CommandParameter.ToString());
            ((MainWindow)Window.GetWindow(this)).ShowControl(new ViewOutgoingLetter(letterId));
        }
    }
}
