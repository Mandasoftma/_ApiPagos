using ApiPagos.Models.Logistica;
using System;
using System.Threading.Tasks;

namespace ApiPagos.Core.Interface
{
    public interface ILogisticaService
    {
        Task<ResponseLogisticaDto> GenerarLogistica(Logistica request);        
        Task<ResponseLogisticaDto> Consultar(Guid id);
    }
}
