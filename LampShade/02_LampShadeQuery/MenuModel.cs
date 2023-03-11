using _02_LampShadeQuery.Contracts.ArticleCategory;
using _02_LampShadeQuery.Contracts.ProductCategory;

namespace _02_LampShadeQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategoryQueries { get; set; }
        public List<ProductCategoryQueryModel> ProductCategoryQueris { get; set; }
    }
}
