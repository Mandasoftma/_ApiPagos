using System;
using System.Threading.Tasks;
using ApiLogística.DAL;
using ApiLogística.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogística.Controllers
{
    [Route("api/Logistica")]
    [ApiController]
    public class LogisticaController : ControllerBase
    {
        private readonly ILogisticaProvider _logistica;

        public LogisticaController(ILogisticaProvider logistica)
        {
            _logistica = logistica;
        }


        [HttpPost("Crear")]
        public async Task<IActionResult>CrearLogistica([FromBody] Logistica request)
        {
            var resp = await _logistica.InsertEnvioAsync(request);
            return Ok(resp);
        }

        [HttpGet("Consultar/{id}")]
        public async Task<IActionResult>ConsultarLogistica(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
                return BadRequest();

            var resp = await _logistica.GetGuiaAsync(id);
            return Ok(resp);
        }

        [HttpGet("Consultar/FacturaId={id}")]
        public async Task<IActionResult> ConsultarLogistica(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
                return BadRequest();

            var resp = await _logistica.GetGuiaPorFacturaAsync(id);
            return Ok(resp);
        }
    }
}
