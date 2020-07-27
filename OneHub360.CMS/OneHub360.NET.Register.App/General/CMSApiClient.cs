using Newtonsoft.Json;
using OneHub360.CMS.DAL;
using OneHub360.Register.App.Properties;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OneHub360.Register.App
{
    public class CMSApiClient : HttpClient
    {
        public CMSApiClient()
        {
            BaseAddress = new Uri(Settings.Default.CMSAPIBaseUrl);
        }

        public Task<bool> RegisterIncomingLetter(IncomingLetter incomingLetter)
        {
            var result = false;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = this.PostAsJsonAsync("cms/incomingletter/register", incomingLetter).Result;

                result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
            }

            return Task.FromResult(result);
        }

        public IList<ExternalOrganizations> GetExternalOrganizations()
        {
            var response = GetStringAsync("cms/incomingletter/getexternalorganizations");
            return JsonConvert.DeserializeObject<IList<ExternalOrganizations>>(response.Result);
        }

        public IList<IncomingLetterView> GetRegisteredLetters()
        {
            var response = GetStringAsync("cms/incomingletter/getregistered");
            return JsonConvert.DeserializeObject<IList<IncomingLetterView>>(response.Result);
        }

        public IList<OutgoingLetterView> GetAllOutgoing()
        {
            var response = GetStringAsync("cms/outgoingletter/getall");
            return JsonConvert.DeserializeObject<IList<OutgoingLetterView>>(response.Result);
        }
    }
}
