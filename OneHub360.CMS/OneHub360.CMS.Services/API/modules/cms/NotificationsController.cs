using OneHub360.CMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Telegram.Bot.Types;

namespace OneHub360.CMS.Services.API.modules.cms
{
    public class NotificationsController : ApiController
    {
        [HttpGet]
        [Route("cms/telegram/send/{message}")]
        public async Task<string> SendMessage(string message)
        {
            var telegramGateway = new TelegramGateway();
            var result = await telegramGateway.SendMessage(message);

            return result.MessageId.ToString();
        }
    }
}
