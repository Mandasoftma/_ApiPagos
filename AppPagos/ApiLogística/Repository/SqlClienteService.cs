using ApiLogística.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogística.Repository
{
    public class SqlClienteService: ISqlClienteService
    {
        private readonly DbeContext _context;
        private readonly ILogger<SqlClienteService> _logger;

        public SqlClienteService(DbeContext context,ILogger<SqlClienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Logistica> GetCliente(string id)
        {
            var cli = await _context.Clientes.SingleAsync(c => c.Id.Equals(id));
            return cli;
        }
    }
}
