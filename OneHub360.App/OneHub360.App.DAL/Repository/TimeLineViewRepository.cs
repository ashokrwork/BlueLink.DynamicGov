using NHibernate.Transform;
using OneHub360.App.Shared;
using OneHub360.DB;
using System;
using System.Collections.Generic;

namespace OneHub360.App.DAL
{
    public class TimeLineViewRepository : NHEntityRepository<TimeLineView>
    {
        public TimeLineViewRepository(IDBContext context) : base(context)
        {
        }

        public IList<TimeLineView> KeyWordSearch(SearchCriteria searchCriteria)
        {
            var query = Context.Session.GetNamedQuery("SimpleSearch");

            query.SetParameter("keyword", searchCriteria.Keyword);
            query.SetParameter("user", searchCriteria.UserId);
            query.SetParameter("feedType", searchCriteria.Feedtype);
            query.SetParameter("dateFrom", searchCriteria.DateFrom);
            query.SetParameter("dateTo", searchCriteria.DateTo);

            query.SetResultTransformer(
                        Transformers.AliasToBean(typeof(TimeLineView)));


            return query.List<TimeLineView>();
        }
    }
}
