using System;

namespace OneHub360.NET.Core.Model
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
