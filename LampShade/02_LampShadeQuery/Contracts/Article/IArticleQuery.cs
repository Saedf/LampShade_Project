namespace _02_LampShadeQuery.Contracts.Article;

public interface IArticleQuery
{
    List<ArticleQueryModel> LatestArticles();
    ArticleQueryModel GetArticleDetails(string slug);

}