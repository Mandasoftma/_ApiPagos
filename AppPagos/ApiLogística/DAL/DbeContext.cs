using ApiLogística.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLogística.DAL
{
    public partial class DbeContext:DbContext
    {
        public DbeContext(DbContextOptions<DbeContext> options) : base(options)
        {
        }

        public virtual DbSet<Logistica> logistica { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Logistica>();
        }
    }
}
