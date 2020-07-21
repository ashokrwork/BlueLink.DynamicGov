using AppSoftware.LicenceEngine.Common;
using AppSoftware.LicenceEngine.KeyVerification;
using Sample1.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialGenerator
{
    public partial class Generator : Form
    {
        public Generator()
        {
            InitializeComponent();
        }
        static List<int> ConvertTextToBinary(int number, int Base)
        {
            List<int> list = new List<int>();
            while (number != 0)
            {
                list.Add(number % Base);
                number = number / Base;
            }
            list.Reverse();
            return list;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // In this sample, we will use the user id as our seed. This could be any integer that you can retrive in the future,
            // such as application id or even a combination of user id and application id


            int userId = int.Parse(txtuserID.Text);

            // We will use the LicenceServer project in this sample to generate a key.
            // In production code, the licence server mechanism would be a separate application
            // from the application that verifies the key. This is important so as to protect the
            // full KeyByteSet array that was used to generate the key.

            var licenceServer = new LicenceServer();

            // Generate the licence key using the seed

            string licenceKeyStr = licenceServer.GenerateLicenceKey(userId);

            Console.WriteLine("\nLicence key generated: " + licenceKeyStr);
            Console.WriteLine("\nNow we will verify the licence key. Please type the licence key printed above: ");
            byte[] toBytes = Encoding.ASCII.GetBytes(licenceKeyStr);

            var str = Convert.ToBase64String(toBytes);

            txtSerial.Text = str;


         
        }
    }
}
