using System;
using System.Threading.Tasks;
using ApiLogística.Models;

namespace ApiLogística.DAL
{
    public interface ILogisticaProvider
    {
        Task<ResponseLogistica> GetGuiaAsync(int id);
        Task<ResponseLogistica> InsertEnvioAsync(Logistica entity);
        Task<ResponseLogistica> GetGuiaPorFacturaAsync(Guid id);
    }
}
