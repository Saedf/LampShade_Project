namespace _02_LampShadeQuery.Contracts.ArticleCategory;

public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
}