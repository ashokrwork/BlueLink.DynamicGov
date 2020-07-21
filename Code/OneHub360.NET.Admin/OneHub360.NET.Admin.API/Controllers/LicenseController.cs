using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using OneHub360.NET.Admin.API.Code;
using OneHub360.NET.Admin.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppSoftware.LicenceEngine.Common;
using AppSoftware.LicenceEngine.KeyVerification;
using Sample1.Server;
using AppSoftware.LicenceEngine.KeyGenerator;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class LicenseController : ApiController
    {
        [HttpGet]
        public bool CheckLicense()
        {
            //read license text in 64 format
            var filetext = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "License/License.lic");
            //var binary = new List<List<int>>();
            //string[] binarystrings = filetext.Split();
            //for (var counter = 0; counter != binarystrings.Length; counter++)
            //{
            //    List<int> a = ConvertTextToBinary(filetext[counter], 2);
            //    binary.Add(a);
            //}
           //convert it into array of bytes
            var strbytes = Convert.FromBase64String(filetext);
            //Convert the array of bytes 
            string license = System.Text.Encoding.UTF8.GetString(strbytes);

            var result = PkvLicenceKeyResult.KeyInvalid;

            var pkvKeyCheck = new PkvKeyCheck();

            // Here we recreate a subset of the full original KeyByteSet array that
            // was used to create the licence key. Note the argument to keyByteNo in 
            // each matches that in the full KeyByteSet array.

            var keyBytes = new[] {

                    new KeyByteSet(5, 165, 15, 132),
                    new KeyByteSet(6, 128, 175, 213)
                };

            result = pkvKeyCheck.CheckKey(license, keyBytes, 8, null);

            if (result != PkvLicenceKeyResult.KeyGood)
            {
                return false;
            }
            else
         
            
            {
                return true;
            }
        }
        [HttpPost]
        public virtual void UploadLicense(string License)
        {
         
        }
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
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

        static string BinaryToText(List<List<int>> seq)
        {
            return new String(seq.Select(s => (char)s.Aggregate((a, b) => a * 2 + b)).ToArray());
        }
        // Use any sort of encoding you like. 
        //var binaryString = ToBinary(ConvertToByteArray("Welcome, World!", Encoding.ASCII));

    }

}
