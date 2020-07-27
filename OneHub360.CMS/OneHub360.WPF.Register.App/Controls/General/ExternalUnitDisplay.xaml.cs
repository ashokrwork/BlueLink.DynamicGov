using OneHub360.CMS.DAL;
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
    /// Interaction logic for ExternalUnitDisplay.xaml
    /// </summary>
    public partial class ExternalUnitDisplay : UserControl
    {
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public Guid OrgUnitId
        {
            get { return (Guid)GetValue(OrgUnitIdProperty); }
            set {
                SetValue(OrgUnitIdProperty, value); }
        }

        public static DependencyProperty OrgUnitIdProperty =
            DependencyProperty.Register("OrgUnitId", typeof(Guid),
            typeof(ExternalUnitDisplay), new UIPropertyMetadata(Guid.Empty, OrgUnitIdPropertyChangedCallback));

        private static void OrgUnitIdPropertyChangedCallback(
   DependencyObject d,
   DependencyPropertyChangedEventArgs e)
        {
            // Cast the originator of the event to your type...
            ExternalUnitDisplay originator = d as ExternalUnitDisplay;
            if (originator != null)
            {
                originator.OnOrgUnitIdPropertyChanged(e);
            }
        }

        private void OnOrgUnitIdPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var orgUnit = ((App)Application.Current).ExternalOrganizationSource.FirstOrDefault(P => P.Id == OrgUnitId);

            if (orgUnit != null)
            {
                lblOrgTitle.Content = orgUnit.Title;
                lblTitle.Content = orgUnit.PersonTitle;

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(orgUnit.LogoUrl, UriKind.Absolute);
                bitmap.EndInit();


                imgOrganization.Source = bitmap;
            }
        }

        public ExternalUnitDisplay()
        {
            InitializeComponent();

            
        }

        
    }
}
