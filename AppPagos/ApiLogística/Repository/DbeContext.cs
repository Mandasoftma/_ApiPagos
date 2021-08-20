using ApiLogística.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLogística.Repository
{
    public partial class DbeContext:DbContext
    {
        public DbeContext(DbContextOptions<DbeContext> options) : base(options)
        {

        }

        public virtual DbSet<Logistica> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logistica>();
        }
    }
}
