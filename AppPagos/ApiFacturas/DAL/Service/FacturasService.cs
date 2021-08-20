using ApiFacturas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFacturas.DAL.Service
{
    public class FacturasService: IFacturasService
    {
        private readonly DbeContext _context;
        public FacturasService(DbeContext context)
        {
            _context = context;
        }
        
        public async Task<ResponseFacturas> InsertFacturaAsync(RequestFactura entity)
        {           
            using var dbTransacion = _context.Database.BeginTransaction();
            try {
                var idFactura = Guid.NewGuid();
                var itemFact = new List<ResponseItemsFactura>();                
                var (items, totalFactura) = await InsertItenFacturas(entity.Productos, idFactura);

                foreach (var i in items)
                {
                    var item = new ResponseItemsFactura
                    {
                        id = i.id,
                        ProductoId = i.ProductoId,
                        Cantidad = i.Cantidad,
                        Precio = i.Precio
                    };
                    itemFact.Add(item);
                }

                var _entity = new Factura
                {
                    Id = idFactura,
                    FechaFactura = DateTime.Now,
                    ClienteId = entity.ClienteId,
                    Total = totalFactura
                };
                var nn =  await _context.Set<Factura>().AddAsync(_entity);
                await _context.SaveChangesAsync();

                dbTransacion.Commit();

                return new ResponseFacturas
                {
                    factura = _entity,
                    productos = itemFact
                };
            }
            catch(Exception e)
            {
                dbTransacion.Rollback();
                Console.WriteLine(e.Message);
            }
            return null;
        }

        private async Task<(List<ItemFactura> items,double Total)> InsertItenFacturas(List<RequestItemFactura> Productos,Guid idFactura)
        {
            double totalFactura = 0;
            var itemFact = new List<ItemFactura>();
            foreach (var i in Productos)
            {
                var item = new ItemFactura
                {
                    FacturaId = idFactura, 
                    id = i.id,
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    Precio = i.Precio
                };
                totalFactura = +(i.Precio * i.Cantidad);
                itemFact.Add(item);
                var resul = await _context.ItemFacturas.AddAsync(item);
               
            }
            return (itemFact, totalFactura);
        }

        public async Task<ResponseFacturas> GetFactura(Guid id)
        {
            var _Factura = new ResponseFacturas();
            try
            {
               var _factura = await _context.Facturas.Where(f => f.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
                var _Productos = await (from f in _context.Facturas
                                  join d in _context.ItemFacturas on f.Id equals d.FacturaId
                                  where f.Id.Equals(id)
                                  select new
                                  {
                                      d.id,
                                      d.ProductoId,
                                      d.Cantidad,
                                      d.Precio
                                  }).AsNoTracking().ToListAsync();


                var itemFact = new List<ResponseItemsFactura>();
                foreach (var i in _Productos)
                {
                    var _items = new ResponseItemsFactura
                    {
                        Cantidad = i.Cantidad,
                        ProductoId = i.ProductoId,
                        Precio = i.Precio
                    };
                    itemFact.Add(_items);
                }

                 _Factura = new ResponseFacturas
                {
                    factura =  _factura,
                    productos = itemFact
                };
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return await Task.FromResult(_Factura);
        }
    }
}
