using OneHub360.App.Business;
using OneHub360.App.Services.Properties;
using OneHub360.App.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OneHub360.Authentication.Context;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;

namespace OneHub360.App.Services.Controllers
{
    [RoutePrefix("api/user")]
    public  class UserInfoController : ApiController
    {
        [HttpGet]
        [Route("search/{keyword}")]
        public virtual IList<UserInfoAutoComplete> SearchUsers(string keyword)
        {
            using(var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
                {
                return userInfoWorker.Search(keyword);
            }
        }

        [HttpGet]
        [Route("aviator/{id}")]
        public virtual UserInfoAutoComplete GetAvaitor(Guid id)
        {
            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                return userInfoWorker.GetAviator(id);
            }
        }

        [HttpGet]
        [Route("autocomplete")]
        public virtual dynamic GetAllAutoComplete()
        {
            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                return userInfoWorker.GetAllAutoComplete();
            }
        }

        [HttpGet]
        [Route("Smartautocomplete")]
        public virtual dynamic GetSmartAutoComplete(string userid)
        {
            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                return userInfoWorker.GetSmartAutoComplete(userid);
            }
        }
        [HttpGet]
        [Route("corpautocomplete")]
        public virtual dynamic GetcorpAutoComplete(string userid)
        {
            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                return userInfoWorker.GetcorpAutoComplete(userid);
            }
        }
        [HttpGet]
        [Route("picture/{id}")]
        public virtual HttpResponseMessage GetUserPicture(Guid id)
        {
            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                var user = userInfoWorker.GetUser(id);

                if (user.Photo == null)
                {
                    user.Photo = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/img/components/autocomplete/nopic.png"));
                }

                var stream = new MemoryStream(user.Photo);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                return response;
            }
        }

        [HttpPost]
        [Route("signature")]
        public virtual bool RegisterDigitalSignature([FromBody]JObject postData) //string signatureImage,string certificateRawData,string certificatePrivateKey)
        {

            dynamic data = postData;

            var certificateRawData = data.certificateRawData.ToString();
            var certificatePrivateKey = data.certificatePrivateKey.ToString();
            var signatureImage = data.signatureImage.ToString();
            var signatureTitle = data.title.ToString();
            var userId = data.userId.ToString();

            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                userInfoWorker.RegisterDigitalSignature(userId, certificateRawData, certificatePrivateKey, signatureImage, signatureTitle);
            }

                return true;


        }

        [HttpGet]
        [Route("signature/{userId}")]
        public virtual Signature GetUserSignature(Guid userId)
        {
            Signature userSignature;

            using (var userInfoWorker = new UserInfoWorker(Settings.Default.WorkerMode))
            {
                userSignature = userInfoWorker.GetUserSignature(userId);
            }

            return userSignature;
        }
    }
}
