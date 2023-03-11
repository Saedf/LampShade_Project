using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.ArticleCategory;
using _02_LampShadeQuery.Contracts.Query;
using _02_LampShadeQuery.Query;
using BlogManagement.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure.Core
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddScoped<IArticleCategoryApplication, ArticleCategoryApplication>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));

        }
    }
}
