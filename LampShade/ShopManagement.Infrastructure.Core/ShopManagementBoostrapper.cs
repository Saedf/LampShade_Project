using _01_Framework.Domain;
using _02_LampShadeQuery.Contracts.ProductCategory;
using _02_LampShadeQuery.Contracts.Query;
using _02_LampShadeQuery.Contracts.Slider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Infrastructure.Core
{
    public class ShopManagementBoostrapper
    {
        public static void Configure(IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication,ProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ISliderApplication, SliderApplication>();

            services.AddTransient<ISliderQuery, SliderQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

        }

    }
}