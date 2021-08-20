using ApiPagos.Core.Interface;
using ApiPagos.Infrastruture;
using ApiPagos.Models.Facturas;
using ApiPagos.Models.Logistica;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiPagos.Core.Service
{
    public class LogisticaService : ILogisticaService
    {
        private readonly IConsumerServices _consumer;
        private readonly IConfiguration _config;
        private readonly string _app = "application/json";

        public LogisticaService(IConsumerServices consumer, IConfiguration config)
        {
            _config = config;
            _consumer = consumer;
        }

        public async Task<ResponseLogisticaDto> GenerarLogistica(Logistica request)
        {
            var _url = _config["Endpoints:Logistica"];
            var _json = JsonConvert.SerializeObject(request);
            StringContent _content = new StringContent(_json, Encoding.UTF8, _app);
            var response = await _consumer.PostDataRest<ResponseLogisticaDto>(string.Format("{0}{1}", _url, "Crear"), _content);
            return response;
        }

        public async Task<ResponseLogisticaDto> Consultar(Guid id)
        {           
            var _url = _config["Endpoints:Logistica"];
            var response = await _consumer.GetDataRest<ResponseLogisticaDto>(string.Format("{0}{1}", _url, "Consultar/FacturaId="), id.ToString());
            return response;
        }
    }
}
