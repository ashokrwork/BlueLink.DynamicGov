using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.Authentication.Context
{
    public class ClaimsContext
    {
        public static string GetCurrentUserId(HttpRequestMessage request)
        {
            ClaimsPrincipal principal = request.GetRequestContext().Principal as ClaimsPrincipal;

            return principal.Claims.Where(c => c.Type == "sub").Single().Value;
        }
    }
}
