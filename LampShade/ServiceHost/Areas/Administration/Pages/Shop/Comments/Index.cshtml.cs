using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Comment;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly ICommentApplication _commentApplication;
        //private readonly IProductApplication _productApplication;
        public List<CommentViewModel> _commentViewModels ;
        public CommentSearchModel SearchModel;

        
        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication=commentApplication;
        }


        public void OnGet(CommentSearchModel searchModel)
        {
           _commentViewModels=_commentApplication.Search(searchModel);
           
        }
        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Canceled(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
           return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirmed(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");
        }





    }
}
