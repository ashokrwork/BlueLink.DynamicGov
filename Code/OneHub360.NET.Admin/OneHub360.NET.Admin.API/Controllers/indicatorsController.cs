using NHibernate;
using NHibernate.Cfg;
using OneHub360.NET.Admin.Model;
using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OneHub360.NET.Admin.API.Controllers
{
    public class indicatorsController : BaseController<UserInfo, NHEntityRepository<UserInfo>>
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage get()
        {
            try
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession sess = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = sess.BeginTransaction())
                    {
                        //Query over roles
                        var query = "SELECT TOP 1 [UserAutocompletetype] FROM [indicators]";
                        var session = sessionFactory.OpenSession();
                        var queryresult = session.CreateSQLQuery(query)
                                         .UniqueResult();
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, queryresult);

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 1);
            }

        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Update([FromUri]short value)
        {

            try
            {
                var sessionFactory = new Configuration().Configure().BuildSessionFactory();
                using (ISession sess = sessionFactory.OpenSession())
                {
                    using (ITransaction tx = sess.BeginTransaction())
                    {
                        //Query over roles
                        var query = "update [indicators] set [UserAutocompletetype] = :value ";
                        var session = sessionFactory.OpenSession();
                        var queryresult = session.CreateSQLQuery(query)
                            .SetInt16("value", value).ExecuteUpdate();
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, queryresult);

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, 0);
            }
        }
    }
}