using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yammer.api;

namespace OneHub360.CMS.Business
{
    public class YammerGateway
    {
        public async static Task<bool> SendMessage(string message)
        {
            return true;
            return await SendMessage(message, ModuleConstants.YammerDemoAccount);
        }
            public static async Task<bool> SendMessage(string message,long userId)
        {
            return true;
            var yammerClient = new YammerClient("5861142-W4ajL4x7SG0bfBgCPNnUdQ");
            await yammerClient.PostInstantMessageAsync(message, userId);
            return true;
        }

        public static List<Message> GetUserMessages()
        {
            var yammerClient = new YammerClient("5861142-W4ajL4x7SG0bfBgCPNnUdQ");
            return yammerClient.RetrieveInstantMessages(DateTime.Now.AddDays(-1).ToUniversalTime());
        }
    }
}
