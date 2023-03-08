using _02_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel ProductQueryModel;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            ProductQueryModel = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment command,string productSlug)
        {
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Product", new { Id = productSlug });
        }
    }
}
