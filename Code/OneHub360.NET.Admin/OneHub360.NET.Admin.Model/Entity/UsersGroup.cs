using OneHub360.NET.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class UsersGroup : NHEntity 
    {

        public virtual System.Guid Fk_Groups { get; set; }
        public virtual System.Guid FK_UserInfo { get; set; }
    }
}
