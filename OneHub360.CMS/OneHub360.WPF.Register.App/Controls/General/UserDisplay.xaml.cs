using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OneHub360.WPF.Register.App.Controls.General
{
    /// <summary>
    /// Interaction logic for UserDisplay.xaml
    /// </summary>
    public partial class UserDisplay : UserControl
    {

        [Bindable(BindableSupport.Yes,BindingDirection.TwoWay)]
        public Guid UserId
        {
            get { return (Guid)GetValue(UserIdProperty); }
            set
            {
                SetValue(UserIdProperty, value);
            }
        }

        public static DependencyProperty UserIdProperty =
            DependencyProperty.Register("UserId", typeof(Guid),
            typeof(UserDisplay), new UIPropertyMetadata(Guid.Empty, OrgUnitIdPropertyChangedCallback));

        private static void OrgUnitIdPropertyChangedCallback(
   DependencyObject d,
   DependencyPropertyChangedEventArgs e)
        {
            // Cast the originator of the event to your type...
            UserDisplay originator = d as UserDisplay;
            if (originator != null)
            {
                originator.OnUserIdPropertyChanged(e);
            }
        }

        private void OnUserIdPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var user = ((App)Application.Current).InternalUsersSource.FirstOrDefault(P => P.Id == UserId);

            if (user != null)
            {
                lblOrgUnitTitle.Content = user.About;
                lblTitle.Content = user.FullName;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(user.Picture, UriKind.Absolute);
                bitmap.EndInit();


                imgProfilePic.Source = bitmap;
            }
        }

        public UserDisplay()
        {
            InitializeComponent();
        }
    }
}
