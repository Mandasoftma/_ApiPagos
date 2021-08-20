using System;
namespace ApiLogística.Models
{
    public class Logistica
    {
        public int Id { get; set; }
        public Guid IdFactura { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
    }
}
