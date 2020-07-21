using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OneHub360.NET.Admin._4.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ltrl.Text= System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
    }
}