using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiPagos.Infrastruture
{
    public interface IConsumerServices
    {
        Task<T> PostDataRest<T>(string _url, StringContent _content) where T : class, new();
        Task<T> GetDataRest<T>(string _url, string _content) where T : class, new();
    }
}
