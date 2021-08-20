using ApiLogística.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogística.DAL
{
    public class LogisticaProvider : ILogisticaProvider
    {
        private readonly DbeContext _context;

        public LogisticaProvider(DbeContext context)
        {
            _context = context;
        }

        public async Task<ResponseLogistica> GetGuiaAsync(int id)
        {
            var response = new ResponseLogistica();
            var _envio = await _context.logistica.Where(f => f.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            response = await ProcesarFactura(_envio);
            return response;
        }


        public async Task<ResponseLogistica> GetGuiaPorFacturaAsync(Guid id)
        {
            var response = new ResponseLogistica();
            var _envio = await _context.logistica.Where(l => l.IdFactura.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            response = await ProcesarFactura(_envio);            
            return response;
        }

        private async Task<ResponseLogistica> ProcesarFactura(Logistica _envio)
        {
            var response = new ResponseLogistica();
            if (_envio == null)
            {
                response.Id = 0;
                response.Direccion = null;
                response.Ciudad = null;
                response.Descripcion = "Guía no encontrada";
                response.Estado = false.ToString();
            }
            else
            {
                response.Id = _envio.Id;
                response.Direccion = _envio.Direccion;
                response.Ciudad = _envio.Ciudad;
                response.Descripcion = "Guía enviá de forma exitosa";
                response.Estado = true.ToString();
            }
           return await Task.FromResult(response); 
        }
        public async Task<ResponseLogistica> InsertEnvioAsync(Logistica entity)
        {
            using var dbTransacion = _context.Database.BeginTransaction();
            var reponse = new ResponseLogistica();
            try
            {
                var resp = await _context.Set<Logistica>().AddAsync(entity);
                await _context.SaveChangesAsync();

                dbTransacion.Commit();

                reponse.Id = resp.Entity.Id;
                reponse.Direccion = resp.Entity.Direccion;
                reponse.Ciudad = resp.Entity.Ciudad;
                reponse.Descripcion = "Guía generada de forma exitosa";
                reponse.Estado = true.ToString();

            }
            catch (Exception e)
            {
                dbTransacion.Rollback();
                reponse.Descripcion = "Algo salio mal la guia no se genero de forma exitosa";
                reponse.Estado = false.ToString();
                Console.WriteLine(e.Message);
            }
            return reponse;
        }

    }
}
