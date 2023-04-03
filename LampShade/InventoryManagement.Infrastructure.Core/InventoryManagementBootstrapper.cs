using _01_Framework.Infrastructure;
using _02_LampShadeQuery.Inventory;
using _02_LampShadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Core.Permissions;
using InventoryManagement.Infrustructure.EfCore;
using InventoryManagement.Infrustructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Core
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
            services.AddTransient<IInventoryQuery, InventoryQuery>();
            
            services.AddDbContext<InventoryContext>(x=>x.UseSqlServer(connectionString));

        }
    }
}