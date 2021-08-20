using System;

namespace ApiPagos.Models.Facturas
{
    public class ResponsePago
    {
        public string codigo { get; set; }
        public Guid facturaId { get; set; }
        public string descripcion { get; set; }
    }
}
