using ApiPagos.Models.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPagos.Core.Interface
{
    public interface IFacturasService
    {      
        Task<ResponseFacturasDto> GenerarFactura(FacturaDto request);
        Task<ResponseFacturasDto> ConsultarPorIdFactura(Guid Id);      
    }
}
