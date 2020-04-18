using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPescador.GoogleReCAPTCHA
{
    public class ReCAPTCHAServices
    {
        private ReCAPTCHASettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        public ReCAPTCHAServices(IOptions<ReCAPTCHASettings> settings, IHttpClientFactory clientFactory)
        {
            _settings = settings.Value;
            _clientFactory = clientFactory;
        }
        public async Task<ReCAPTCHAResponse> Check(string token)
        {
            var data = new ReCAPTCHAData
            {
                response = token,
                secret = _settings.ReCAPTCHA_Secret_Key
            };

            var client = _clientFactory.CreateClient();
            var response = await client.GetStringAsync("https://www.google.com/recaptcha/api/siteverify?secret=" + data.secret + "&response=" + data.response);
            var result = JsonConvert.DeserializeObject<ReCAPTCHAResponse>(response);           

            return result;
        }
    }
}
