using OneHub360.CMS.DAL;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OneHub360.CMS.Business
{
    public class TelegramGateway
    {
        public async Task<Message> SendMessage(string message)
        {
            return new Message();

            var client = new TelegramBotClient("243607587:AAEqGos47W0gfMTMIFkdxkiT7vxMwU_sk4c");

            

            var result = await client.SendTextMessageAsync(ModuleConstants.Demo.ChatId, message);

            return result;
        }
    }
}
