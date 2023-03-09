using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        EditArticle GetDetailsBy(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        Article GetArticleWithCategoryBy(long id);
    }
}
