using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class EditModel : PageModel
    {
        public EditArticle Command;
        public SelectList ArticleCategories;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;

        public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication)
        {
            _articleApplication = articleApplication;
            _categoryApplication = categoryApplication;
        }

        public void OnGet(long id)
        {
            Command = _articleApplication.GetDetails(id);
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(),
                "Id","Name");

        }

        public IActionResult OnPost(EditArticle command)
        {
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
