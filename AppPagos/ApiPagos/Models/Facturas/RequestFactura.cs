using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPagos.Models.Facturas
{
    public class RequestFactura
    {
        public DateTime FechaFactura { get; set; }
        public int ClienteId { get; set; }
        public double Total { get; set; }

        //public List<ItemFactura> Items { get; set; }
    }
}
