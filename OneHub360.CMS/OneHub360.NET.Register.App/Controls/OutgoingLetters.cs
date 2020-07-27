using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OneHub360.Register.App.Controls
{
    public partial class OutgoingLetters : UserControl
    {
        public OutgoingLetters()
        {
            InitializeComponent();
            var thread = new Thread(InitControl);
            thread.Start();
        }

        private void InitControl()
        {
            if (metroGridRegisteredLetters.InvokeRequired)
            {
                metroGridRegisteredLetters.Invoke(new MethodInvoker(delegate
                {
                    metroGridRegisteredLetters.AutoGenerateColumns = false;
                    tileRegisteredLetters.Text = "جاري التحميل";
                    var registeredLetters = new CMSApiClient().GetAllOutgoing();
                    metroGridRegisteredLetters.DataSource = registeredLetters;
                    tileRegisteredLetters.Text = "قائمة الكتب الصادرة";
                }));
            }
            else
            {
                metroGridRegisteredLetters.AutoGenerateColumns = false;
                tileRegisteredLetters.Text = "جاري التحميل";
                var registeredLetters = new CMSApiClient().GetAllOutgoing();
                metroGridRegisteredLetters.DataSource = registeredLetters;
                tileRegisteredLetters.Text = "قائمة الكتب الصادرة";
            }
        }
    }
}
