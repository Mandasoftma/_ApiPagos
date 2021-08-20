using System;
using System.Threading.Tasks;
using ApiFacturas.DAL.Service;
using ApiFacturas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiFacturas.Controllers
{
    [Route("api/Facturas")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly ILogger<FacturasController> _logger;
        private readonly IFacturasService _factura;
        public FacturasController(IFacturasService factura, ILogger<FacturasController> logger)
        {
            _factura = factura; ;
            _logger = logger;
        }


        [HttpPost("Crear")]
        public async Task<IActionResult> GuardarFactura([FromBody] RequestFactura request)
        {            
            var resp = await _factura.InsertFacturaAsync(request);
            return Ok(resp);
        }

        [HttpGet("Consultar/Factura={Id}")]
        public async Task<IActionResult> GetFacturaxid(Guid id)
        {
            var cli = await _factura.GetFactura(id);
            if (cli.factura != null)
                return Ok(cli);

            return NotFound();
        }
    }
}
