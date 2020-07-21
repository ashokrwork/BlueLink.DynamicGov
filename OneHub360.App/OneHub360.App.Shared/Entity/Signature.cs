using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Shared
{
    public partial class Signature : AdminNHEntity
    {
        public virtual string Title { get; set; }

        public virtual Guid FK_UserInfo { get; set; }

        public virtual byte[] Image { get; set; }
        
        public virtual DateTime EnrollmentDate { get; set; }
        
        public virtual DateTime? NotAfter { get; set; }
        
        public virtual DateTime? NotBefore { get; set; }
        
        public virtual string Certificate { get; set; }
        
        public virtual int Status { get; set; }
        public virtual string SMSActivationCode { get; set; }
        public virtual string EmailActivationCode { get; set; }
        public virtual int? ActivationFailureCount { get; set; }
        public virtual bool? IncludePrivateKey { get; set; }
        
        public virtual string PrivateKey { get; set; }

        public virtual X509Certificate2 SingingCertificate { get
            {
                if(string.IsNullOrEmpty(Certificate) && string.IsNullOrEmpty(PrivateKey))
                {
                    return null;
                }
                else
                {
                    try
                    {
                        var certificate = new X509Certificate2(Convert.FromBase64String(Certificate));
                        var rsa = new RSACryptoServiceProvider();
                        rsa.FromXmlString(PrivateKey);

                        certificate.PrivateKey = rsa;

                        return certificate;
                    }
                    catch
                    {
                        return null;
                    }
                }
            } }
    }
}
