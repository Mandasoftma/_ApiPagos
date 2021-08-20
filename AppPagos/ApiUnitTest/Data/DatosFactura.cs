using ApiPagos.Models.Facturas;
using ApiPagos.Models.Logistica;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiUnitTest.Data
{
    public class DatosFactura
    {
        readonly FacturaDto requestFacturas;
        public ResponseFacturasDto responseFacturas { get; set; }

        public DatosFactura()
        {
            requestFacturas = new FacturaDto
            {
                ClienteId = 300,
                Productos = new List<ItemFacturaDto>()
                {
                    new ItemFacturaDto {
                        Id = 1,
                        ProductoId =3,
                        Cantidad = 20,
                        Precio = 600
                    }
                }
            };

            responseFacturas = new ResponseFacturasDto
            {
                factura = new ResponceFactura
                {
                    Id = Guid.Parse("9D6872FD-5C92-4443-843F-69FEBCCAC958"),
                    FechaFactura = DateTime.Now,
                    ClienteId = 300,
                    Total = (600 * 20)
                },
                Productos = new List<ItemFacturaDto>() {
                    new ItemFacturaDto {
                            Id = 1,
                            ProductoId =3,
                            Cantidad = 20,
                            Precio = 600
                    }
                },
                Logistica = new ResponseLogisticaDto
                {
                    Id = 100,
                    Direccion = "Direccion de pruebas",
                    Ciudad = "Cali",
                    Descripcion = "Ok",
                    Estado = "true"
                }
            };
        }

        public FacturaDto RequestFacturas() =>
            requestFacturas;

        public ResponseFacturasDto ResponseFacturas() =>
            responseFacturas;
    }
}
