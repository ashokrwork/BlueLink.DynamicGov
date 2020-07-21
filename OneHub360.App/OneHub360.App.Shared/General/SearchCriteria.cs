using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Shared
{
    public class SearchCriteria
    {
        private DateTime? _dateFrom;

        public string Keyword { get; set; }
        public string UserId { get; set; }
        public Guid? Feedtype { get; set; }
        public DateTime? DateFrom
        {
            get
            {
                if (_dateFrom.HasValue)
                {
                    return new DateTime?(_dateFrom.Value.ToUniversalTime());
                }
                else
                {
                    return new DateTime?();
                }
            }
            set
            {
                _dateFrom = value;
            }
        }
        public DateTime? DateTo { get; set; }
    }
}
