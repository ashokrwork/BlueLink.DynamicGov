using OneHub360.App.DAL;
using OneHub360.App.Shared;
using System;
using System.Collections.Generic;
using OneHub360.Business;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Web.Hosting;
using NHibernate;
using NHibernate.Cfg;
using System.IO;

namespace OneHub360.App.Business
{
    public class UserInfoWorker : AdminWorkerBase
    {
        private WorkerMode workerMode;


        public UserInfoWorker(WorkerMode mode) : base(mode)
        {
        }

        public UserInfoWorker(WorkerMode mode, AdminContext context) : base(mode)
        {
            Context = context;
        }

        public virtual IList<UserInfoAutoComplete> Search(string keyWord)
        {
            IList<UserInfoAutoComplete> userInfo;

            var userInfoAutoCompleteRepository = new UserInfoAutoCompleteRepository(Context);
            //long totalCount = 0;
            //return userInfoAutoCompleteRepository.GetPaged(string.Format("ArabicFullName like N''%{0}%''", keyWord), string.Empty,0, 10, out totalCount);
            userInfo = userInfoAutoCompleteRepository.GetAll();



            return userInfo;
        }

        public virtual UserInfoAutoComplete GetAviator(Guid id)
        {
            UserInfoAutoComplete userinfo;

            var userInfoAutoCompleteRepository = new UserInfoAutoCompleteRepository(Context);
            userinfo = userInfoAutoCompleteRepository.GetById(id);


            return userinfo;
        }

        public virtual UserInfos GetUser(Guid id)
        {
            UserInfos userInfo;

            var userInfoRepository = new UserInfoRepository(Context);
            userInfo = userInfoRepository.GetById(id);


            return userInfo;
        }

        public virtual IList<UserInfoAutoComplete> GetAllAutoComplete()
        {
            IList<UserInfoAutoComplete> autoComplete;

            var userInfoAutoCompleteRepository = new UserInfoAutoCompleteRepository(Context);

            //autoComplete = userInfoAutoCompleteRepository.GetPaged(string.Format("Id <> '{0}'",userId), string.Empty, 0, int.MaxValue, out totalCount);
            autoComplete = userInfoAutoCompleteRepository.GetAll();


            return autoComplete;
        }
        public virtual IList<UserInfoAutoComplete> GetSmartAutoComplete(string userid)
        {
            string configFilePath;
            IList<UserInfoAutoComplete> autoComplete= new List<UserInfoAutoComplete>(); ;
            if (!string.IsNullOrEmpty(userid))
            {
                //Query over roles
                var query = "SELECT top 1 [UserAutocompletetype] FROM [indicators]";

                if (HostingEnvironment.ApplicationHost == null)
                    configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\app\\admin.config");
                else
                    configFilePath = HostingEnvironment.MapPath("~/config/app/admin.config");
                ISessionFactory sessionFactory;
                sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();
                var session = sessionFactory.OpenSession();
                var queryresult = session.CreateSQLQuery(query).UniqueResult();

                if (queryresult.ToString().Trim() == "1")
                    return GetAllAutoComplete();
                else if (queryresult.ToString().Trim() == "2")
                    return GetcorpAutoComplete(userid);


            }

            return autoComplete;
        }
        public virtual IList<UserInfoAutoComplete> GetcorpAutoComplete(string userid)
        {
            string configFilePath;
            IList<UserInfoAutoComplete> autoComplete=new List<UserInfoAutoComplete>();

            var userInfoAutoCompleteRepository = new UserInfoAutoCompleteRepository(Context);

            //autoComplete = userInfoAutoCompleteRepository.GetPaged(string.Format("Id <> '{0}'",userId), string.Empty, 0, int.MaxValue, out totalCount);
            if (!string.IsNullOrEmpty(userid))
            {
                //Query over roles
                var query = "SELECT DISTINCT [Id],[ArabicFullName],[About],[Photo],[Email],[PersonalMessage],[Status],[Mobile] " +
                    "FROM            UserInfo AS user1 " +
                    "WHERE(FK_ReportingTo = :userid) OR " +
                " (:userid IN " +
                  "   (SELECT        Id     FROM            UserInfo " +
                     "  WHERE(user1.FK_ReportingTo = FK_ReportingTo))) OR " +
         " (:userid IN (SELECT  Id " +
              " FROM            UserInfo AS UserInfo_1 " +
              " WHERE(FK_ReportingTo = user1.Id))) ";
                if (HostingEnvironment.ApplicationHost == null)
                    configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\app\\admin.config");
                else
                    configFilePath = HostingEnvironment.MapPath("~/config/app/admin.config");
                ISessionFactory sessionFactory;
                sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();
                var session = sessionFactory.OpenSession();
                var queryresult = session.CreateSQLQuery(query)
                                 .SetString("userid", userid)
                                 .List();
                for (int i = 0; i < queryresult.Count; i++)
                {
                    var Object = (object[])queryresult[i];
                    autoComplete.Add(new UserInfoAutoComplete() {Id= new Guid(Object.GetValue(0).ToString()),FullName = Object.GetValue(1).ToString(), About= Object.GetValue(2).ToString(), Mobile = Object.GetValue(7).ToString() });
                } 

            }


            return autoComplete;
        }

        public virtual bool RegisterDigitalSignature(string userId, string certificateRawData, string certificatePrivateKey, string signatureImage, string signatureTitle)
        {
            X509Certificate2 certificate;
            DateTime? Notafter = null,Notbefore = null;
            if (!string.IsNullOrEmpty(certificateRawData))
            {
                 certificate = new X509Certificate2(Convert.FromBase64String(certificateRawData));
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(certificatePrivateKey);

                certificate.PrivateKey = rsa;
                Notafter = certificate.NotAfter;
                Notbefore = certificate.NotBefore;

            }
            else
            {
                certificate = new X509Certificate2();
                certificatePrivateKey = "";
            }
            var signatureRepository = new SignatureRepository(Context);

            var existingSignature = signatureRepository.GetById(Guid.Parse(userId));

            if (existingSignature == null)
            {

                var signature = new Signature()
                {
                    Certificate = certificateRawData,
                    CreatedBy = "",
                    CreationDate = DateTime.Now,
                    EnrollmentDate = DateTime.Now,
                    Image = Convert.FromBase64String(signatureImage),
                    IncludePrivateKey = true,
                    ActivationFailureCount = 0,
                    EmailActivationCode = "",
                    IsDeleted = false,
                    LastModified = DateTime.Now,
                    LastModifiedBy = userId,
                    NotAfter = Notafter,
                    NotBefore = Notbefore,
                    PrivateKey = certificatePrivateKey,
                    SMSActivationCode = "",
                    Status = 1,
                    Title = signatureTitle,
                    FK_UserInfo = Guid.Parse(userId)
                };

                signatureRepository.Insert(signature);
            }

            else
            {
                existingSignature.Certificate = certificateRawData;
                existingSignature.Image = Convert.FromBase64String(signatureImage);
                existingSignature.NotAfter = Notafter;
                existingSignature.NotBefore = Notbefore;
                existingSignature.PrivateKey = certificatePrivateKey;
                existingSignature.Title = signatureTitle;
                existingSignature.LastModified = DateTime.Now;
                existingSignature.LastModifiedBy = userId;

                signatureRepository.Update(existingSignature);

            }

            return true;

        }

        public virtual Signature GetUserSignature(Guid userId)
        {
            var signatureRepository = new SignatureRepository(Context);

            return signatureRepository.GetById(userId);
        }
    }
}
