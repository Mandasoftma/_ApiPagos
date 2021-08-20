using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPagos.Models.Facturas
{
    public class FacturaDto
    {  
        public int ClienteId { get; set; }

        public List<ItemFacturaDto> Productos { get; set; }
    }
}
