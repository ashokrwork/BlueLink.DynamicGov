/*
 * This code belongs to Hewlett Packard Enterprise
 * Copyright © 2016 HPE -  All rights are reserved worldwide
 */

using NHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using NHibernate.Context;
using Newtonsoft.Json.Serialization;

namespace OneHub360.NET.Admin.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ISessionFactory SessionFactory
        {
            get;
            private set;
        }

        protected void Application_Start()
        {
            Controllers.LicenseController cc = new Controllers.LicenseController();
            cc.CheckLicense();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //HttpConfiguration config = GlobalConfiguration.Configuration;
            //((DefaultContractResolver)config.Formatters.JsonFormatter.SerializerSettings.ContractResolver).IgnoreSerializableAttribute = true;

            // NHibernate Configuration
            SessionFactory = new Configuration().Configure().BuildSessionFactory();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Create session
            ISession session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Close session
            ISession session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
        }
    }
}
