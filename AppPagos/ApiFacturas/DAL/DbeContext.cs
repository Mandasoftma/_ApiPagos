using ApiFacturas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiFacturas.DAL
{
    public partial class DbeContext:DbContext
    {
        public DbeContext(DbContextOptions<DbeContext> options) : base(options)
        {
        }

        public virtual DbSet<ItemFactura> ItemFacturas { get; set; }

        public virtual DbSet<Factura> Facturas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>();
            modelBuilder.Entity<ItemFactura>();
        }
    }
}
