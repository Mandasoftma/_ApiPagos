using ApiPagos.Models.Logistica;
using System;
using System.Collections.Generic;

namespace ApiPagos.Models.Facturas
{
    public class ResponseFacturasDto
    {
        public ResponceFactura factura { get; set; }
        public List<ItemFacturaDto> Productos { get; set; }
        public ResponseLogisticaDto Logistica { get; set; }
    }

    public class ResponceFactura
    {
        public Guid Id { get; set; }
        public DateTime FechaFactura { get; set; }
        public int ClienteId { get; set; }
        public double Total { get; set; }
    }
}
