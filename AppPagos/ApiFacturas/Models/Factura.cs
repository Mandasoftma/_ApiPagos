using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturas.Models
{
    public class Factura
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime FechaFactura { get; set; }
        public int ClienteId { get; set; }
        public double Total { get; set; }

        public List<ItemFactura> productos { get; set; }
    }
}
