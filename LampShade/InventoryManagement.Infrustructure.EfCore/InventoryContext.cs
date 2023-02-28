using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrustructure.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrustructure.EfCore
{
    public class InventoryContext:DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
