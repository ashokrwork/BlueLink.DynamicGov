using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Rolesusers : NHEntity
    {
        public virtual Guid FkRole { get; set; }
        public virtual Guid FkUserinfo { get; set; }
        
    }
}
