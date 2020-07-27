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
using OneHub360.NET.Register.App;

namespace OneHub360.Register.App.Controls
{
    public partial class RegiteredLetters : UserControl
    {
        public RegiteredLetters()
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
                    var registeredLetters = new CMSApiClient().GetRegisteredLetters();
                    metroGridRegisteredLetters.DataSource = registeredLetters;
                    tileRegisteredLetters.Text = "قائمة الكتب المسجلة";
                }));
            }
            else
            {
                metroGridRegisteredLetters.AutoGenerateColumns = false;
                tileRegisteredLetters.Text = "جاري التحميل";
                var registeredLetters = new CMSApiClient().GetRegisteredLetters();
                metroGridRegisteredLetters.DataSource = registeredLetters;
                tileRegisteredLetters.Text = "قائمة الكتب المسجلة";
            }
        }

        private void metroGridRegisteredLetters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var clickedColumn = (DataGridViewButtonColumn)senderGrid.Columns[e.ColumnIndex];
                var btnName = clickedColumn.Name;
                

                switch(btnName)
                {
                    case "btnIndexing":
                        var incomingLetterId = Guid.Parse(senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ((MainForm)this.ParentForm).ShowControl(new IndexIncomingLetter(incomingLetterId));
                        break;
                }
            }
        }
    }
}
