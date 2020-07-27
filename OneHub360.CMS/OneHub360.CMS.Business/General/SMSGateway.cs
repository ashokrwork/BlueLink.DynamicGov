using Nexmo.Api;

namespace OneHub360.CMS.Business
{
    public class SMSGateway 
    {
        public SMSGateway() 
        {

        }

        public bool SendSMS(string message, string mobile)
        {
            
            var messageRequest = new SMS.SMSRequest
            {
                from = "OneHub360",
                to = mobile,
                text = message
            };
            var results = SMS.Send(messageRequest);

            return results.messages[0].status == "0";
        }

        public bool SendUnicodeSMS(string message,string mobile)
        {
            return true;
            var messageRequest = new SMS.SMSRequest
            {
                from = "OneHub360",
                to = mobile,
                text = message,
                type = "unicode"
            };
            var results = SMS.Send(messageRequest);

            return results.messages[0].status == "0";
        }
    }
}
