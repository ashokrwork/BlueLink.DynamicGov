using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Business
{
    public class TelegramAPIClient : HttpClient
    {
        public TelegramAPIClient()
        {
            BaseAddress = new Uri("http://localhost:363/");
        }

        public async Task<string> SendMessage(string message)
        {
            return await GetStringAsync(string.Format("cms/telegram/send/{0}", message));
        }
    }
}
