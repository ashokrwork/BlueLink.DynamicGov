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

namespace OneHub360.WPF.Register.App.Controls.Incoming
{
    /// <summary>
    /// Interaction logic for IncomingLettersList.xaml
    /// </summary>
    public partial class IncomingLettersList : UserControl
    {
        public IncomingLettersList()
        {
            InitializeComponent();
            LoadLetters();
        }

        private async void LoadLetters()
        {
            using (var client = new APIClient())
            {
                var registeredLetters = await client.GetRegisteredLetters();
                listIncomingLetters.ItemsSource = registeredLetters;
            }
        }

        

        private void btnIndex_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowControl(new IndexIncomingLetter());
        }
    }
}
