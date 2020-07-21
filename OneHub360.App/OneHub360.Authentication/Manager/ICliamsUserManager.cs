using OneHub360.Authentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.Authentication.Manager
{
    public interface IClaimsUserManager
    {
        Task<CliamsUser> Find(string username, string password);
    }
}
