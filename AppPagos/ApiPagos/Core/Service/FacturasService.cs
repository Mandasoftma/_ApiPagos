using ApiPagos.Core.Interface;
using ApiPagos.Infrastruture;
using ApiPagos.Models.Facturas;
using ApiPagos.Models.Logistica;
using ApiPagos.Models.Utility;
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
    public class FacturasService: IFacturasService
    {
        private readonly IConsumerServices _consumer;
        private readonly IConfiguration _config;
        private readonly ILogisticaService _logistica;
        private readonly string _app = "application/json";

        public FacturasService(IConsumerServices consumer, ILogisticaService logistica,IConfiguration config)
        {
            _config = config;
            _consumer = consumer;
            _logistica = logistica;
        }
        public async Task<ResponseFacturasDto> GenerarFactura(FacturaDto request)
        {
            var _url = _config["Endpoints:Facturas"];
            var _json = JsonConvert.SerializeObject(request);
            StringContent _content = new StringContent(_json, Encoding.UTF8, _app);

            var response = await _consumer.PostDataRest<ResponseFacturasDto>(string.Format("{0}{1}", _url, "Crear"), _content);

            if (response != null)
            {
                var direc = retoraDoreccion();
                var _request = new Logistica
                {
                    IdFactura = response.factura.Id,
                    Direccion = direc.Direccion,
                    Ciudad = direc.Ciudad
                };

                var responseLogistica = await _logistica.GenerarLogistica(_request);
                response.Logistica = responseLogistica;
            }
            return response;
        }

        public async Task<ResponseFacturasDto> ConsultarPorIdFactura(Guid Id)
        {
            var _url = _config["Endpoints:Facturas"];
            var response = await _consumer.GetDataRest<ResponseFacturasDto>(string.Format("{0}{1}", _url, "Consultar/Factura="), Id.ToString());

            if (response.factura != null)
                response.Logistica = await _logistica.Consultar(Id);


            return response;
        }

        private Direcciones retoraDoreccion()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 3)); 

            var random = new Random(seed);
            var value = random.Next(0, 5);
            var _ciudad = ((value % 2)== 0 ? "Medellin" : "Barranquilla");

            var item = new Direcciones
            {
                id = value,
                Direccion = string.Concat("Calle ", seed, " Esquina"),
                Ciudad = ((value % 2) == 0 ? "Medellin" : "Barranquilla")
            };
            return item;
        }
    }
}
