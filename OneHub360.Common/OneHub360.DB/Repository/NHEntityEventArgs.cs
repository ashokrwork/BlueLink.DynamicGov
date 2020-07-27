using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.DB
{
    public class NHEntityEventArgs : EventArgs
    {
        public NHEntityEventArgs()
        {
            Cancel = false;
        }
        public INHEntity Entity { get; set; }
        public Guid EntityId { get; set; }
        public bool Cancel { get; set; }

    }
}
