using System;
using System.Threading.Tasks;
using ApiPagos.Core.Interface;
using ApiPagos.Models.Facturas;
using Microsoft.AspNetCore.Mvc;

namespace ApiPagos.Controllers
{
    [Route("api/Pagos")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IFacturasService _facturas;

        public PagosController( IFacturasService facturas)
        {
            _facturas = facturas;
        }

        [HttpPost("Facturar")]
        public async Task<IActionResult> GenerarPago([FromBody] FacturaDto request)
        {
            if (request == null)
                return BadRequest();
            var response = await _facturas.GenerarFactura(request);
            return Ok(response);
        }
        

        [HttpPost("Consultar/Factura")]
        public async Task<IActionResult> ConsultarFacturaPorIdFactura(ConsultarFactura request)
        {
            if (string.IsNullOrWhiteSpace(request.FacturaId.ToString()))
                return BadRequest();

            var response = await _facturas.ConsultarPorIdFactura(request.FacturaId);          

            return Ok(response);
        }
    }
}
