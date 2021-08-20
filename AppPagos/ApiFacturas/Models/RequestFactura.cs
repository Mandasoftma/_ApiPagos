using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturas.Models
{
    public class RequestFactura
    {        
        public int ClienteId { get; set; }

        public List<RequestItemFactura> Productos { get; set; }
    }
}
