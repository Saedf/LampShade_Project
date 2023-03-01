using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;
        public List<InventoryViewModel> _inventoryViewModels;
        public InventorySearchModel SearchModel;

        public SelectList Products;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(InventorySearchModel searchModel)
        {
           _inventoryViewModels=_inventoryApplication.Search(searchModel);
           Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
        }
        public PartialViewResult OnGetCreate()
        {
            var command = new CreateInventory()
            {
                Products = _productApplication.GetProducts()
            };
           return Partial("./Create",command);
        }

      
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);
        }
        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("Increase",command);

        }
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("Reduce",command);

        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);

        }
    }
}
