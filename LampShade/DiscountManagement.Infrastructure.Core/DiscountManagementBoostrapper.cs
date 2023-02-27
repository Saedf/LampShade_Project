using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Core
{
    public class DiscountManagementBoostrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
         
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();

            services.AddDbContext<DiscountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}