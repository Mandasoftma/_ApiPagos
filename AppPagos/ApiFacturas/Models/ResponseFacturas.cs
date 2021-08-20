using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturas.Models
{
    public class ResponseFacturas
    {
        public Factura factura { get; set; }
        public List<ResponseItemsFactura> productos { get; set; }
    }    
}
