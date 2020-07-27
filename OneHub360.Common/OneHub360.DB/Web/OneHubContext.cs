using NHibernate;
using System.Web;

namespace OneHub360.DB
{
    public class OneHubAppContext
    {
        #region OneHubContext Constructor & Singleton Implementation Members
        private static OneHubAppContext currentContext;
        private OneHubAppContext()
        {
            // Check the HttpContext if contains the Session Factory from the Global API
            if (HttpContext.Current.Items.Contains("SessionFactory"))
                Session = ((ISessionFactory)HttpContext.Current.Items["SessionFactory"]).GetCurrentSession();
        }

        /// <summary>
        /// Get the Current OneHubContext for the current User
        /// </summary>
        public static OneHubAppContext Current
        {
            get
            {
                if (currentContext == null)
                    currentContext = new OneHubAppContext();
                return currentContext;
            }
        }
        #endregion

        #region OneHubContext Public Properties
        public Language CurrentLanguage { get; set; }
        public ISession Session { get; set; }
        public IUserInfo User
        {
            get;
        }
        public string BrowserType
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            }
        }
        public string MachineIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        public string MachineName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            }
        }
        public string ServerName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            }
        }
        #endregion
    }
}
