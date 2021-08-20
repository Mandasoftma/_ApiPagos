using ApiPagos.Infrastruture.CircuitBreaker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiPagos.Infrastruture
{
    public class ConsumerServices : IConsumerServices
    {
        private readonly HttpClient _client;
        private readonly ILogger<ConsumerServices> _ilogger;
        private const int maxAttempts = 3;
        private const int pause = 2;

        public ConsumerServices(HttpClient client, ILogger<ConsumerServices> ilogger)
        {
            _client = client;
            _ilogger = ilogger;
            PollyRetry.Init(ilogger);
        }

        public async Task<T> PostDataRest<T>(string _url, StringContent _content) where T : class, new()
        {
            var retryPolicy = PollyRetry.GetPolicyAsync(maxAttempts, pause, string.Format("{0}", _url));
            var response = new HttpResponseMessage();

            await retryPolicy.ExecuteAsync(async () =>
            {
                response = await _client.PostAsync(_url, _content);
                if (response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            });

            var resul = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            return resul;
        }

        public async Task<T> GetDataRest<T>(string _url,string _content) where T : class, new()
        {
            var retryPolicy = PollyRetry.GetPolicyAsync(maxAttempts, pause, string.Format("{0}", _url));
            var response = new HttpResponseMessage();

            await retryPolicy.ExecuteAsync(async () => {
                response = await _client.GetAsync(string.Format("{0}{1}", _url, _content));
                if (response.IsSuccessStatusCode)
                    response.EnsureSuccessStatusCode();
            });

            var resul = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            return resul;
        }
    }
}
