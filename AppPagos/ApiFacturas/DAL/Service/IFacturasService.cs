using ApiFacturas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturas.DAL.Service
{
    public interface IFacturasService
    {    
        Task<ResponseFacturas> InsertFacturaAsync(RequestFactura entity);
        Task<ResponseFacturas> GetFactura(Guid id);
    }
}
