using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using AccountSearchModel = AccountManagement.Application.Contracts.Account.AccountSearchModel;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IAccountApplication _accountApplication;
        public List<AccountViewModel> _accountViewModels;
        public AccountSearchModel SearchModel;
        private readonly IRoleApplication _roleApplication;

        public SelectList RoleList;
        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            RoleList = new SelectList(_roleApplication.List(),"Id","Name");
            _accountViewModels = _accountApplication.Search(searchModel);

        }
        public PartialViewResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = _roleApplication.List()
            };
           return Partial("./Create",command);
        }

      
        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles=_roleApplication.List();
            return Partial("Edit", account);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword{Id = id};
            return Partial("ChangePassword",command);

        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }
        //public IActionResult OnGetIsInStock(long id)
        //{
        //    var result = _productApplication.IsInStock(id);
        //    if (result.IsSucceeded)
        //    {
        //        return RedirectToPage("./Index");
        //    }
        //    Message = result.Message;
        //    return RedirectToPage("./Index");

        //}
    }
}
