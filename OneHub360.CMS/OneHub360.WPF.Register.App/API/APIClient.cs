using Newtonsoft.Json;
using OneHub360.App.Shared;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OneHub360.WPF.Register.App
{
    public class APIClient : HttpClient
    {
        public APIClient()
        {
            BaseAddress = new Uri("http://onehub360sp:363/");
        }

        public Task<byte[]> ReadFile(string fileUrl)
        {
            return GetByteArrayAsync(fileUrl);
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
                throw ex;
            }

            return Task.FromResult(result);
        }

        public IList<ExternalOrganizations> GetExternalOrganizations()
        {
            var response = GetStringAsync("cms/externalorganizations/getall");
            return JsonConvert.DeserializeObject<IList<ExternalOrganizations>>(response.Result);
        }

        public IList<UserInfoAutoComplete> GetInternalOrganizations()
        {
            var response = GetStringAsync("api/user/autocomplete");
            return JsonConvert.DeserializeObject<IList<UserInfoAutoComplete>>(response.Result);
        }

        public async Task<IList<IncomingLetterView>> GetRegisteredLetters()
        {
            var response = await GetStringAsync("cms/incomingletter/getregistered");
            return JsonConvert.DeserializeObject<IList<IncomingLetterView>>(response);
        }

        public async Task<IList<OutgoingLetterView>> GetAll()
        {
            var response = await GetStringAsync("cms/outgoingletter/getall");
            return JsonConvert.DeserializeObject<IList<OutgoingLetterView>>(response);
        }

        public async Task<IList<OutgoingLetterView>> GetSignedLetters()
        {
            var response = await GetStringAsync("cms/outgoingletter/signed");
            return JsonConvert.DeserializeObject<IList<OutgoingLetterView>>(response);
        }

        public async Task<OutgoingLetterView> GetOutgoingLetter(Guid outgoingLetterId)
        {
            var response = await GetStringAsync("cms/outgoingletter/view/" + outgoingLetterId);
            return JsonConvert.DeserializeObject<OutgoingLetterView>(response);
        }

        public async Task<bool> ExportManually(Guid outgoingLetterId)
        {
            var response = await GetStringAsync("cms/outgoingletter/export/" + outgoingLetterId);
            return true;
        }

        public async Task<IList<DocumentsView>> GetOutgoingLetterAttachements(Guid outgoingLetterId)
        {
            var response = await GetStringAsync("cms/outgoingletter/attachements/" + outgoingLetterId);
            return JsonConvert.DeserializeObject< IList<DocumentsView>>(response); 
        }

        public async Task<bool> ExportG2G(Guid outgoingLetterId)
        {
            var response = await GetStringAsync("cms/outgoingletter/export/g2g/" + outgoingLetterId);
            return true;
        }
    }
}
