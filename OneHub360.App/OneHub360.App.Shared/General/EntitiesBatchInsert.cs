using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Shared
{
    public class EntitiesBatchInsert
    {
        public EntitiesBatchInsert()
        {
            feeds = new List<Feeds>();
            actions = new List<UserAction>();
        }
        public IList<Feeds> feeds { get; set; }
        public IList<UserAction> actions { get; set; }
    }
}
