using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.WebDav.Handlers
{
    public interface IMethodHandler
    {
        //The method that process the requests
        int Handle();
        //The Xml that is generated as a response. Maybe an empty string for some METHODS
        string ResponseXml { get; }
        //The Xml that is recieve and processed from the client. Maybe an empty string for some METHODS
        string RequestXml { get; }
        //Used to return errors on multi-status requests
        string ErrorXml { get; }
    }
}
