using ApiLogística.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogística.Repository
{
    public interface ISqlClienteService
    {
        Task<Logistica> GetCliente(string id);
    }
}
