using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.Slider;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slider
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private readonly ISliderApplication _sliderApplication;
        public List<SliderViewModel> _sliders;

        public IndexModel(ISliderApplication sliderApplication)
        {
            _sliderApplication = sliderApplication;
            
        }


        public void OnGet()
        {
            _sliders = _sliderApplication.GetList();
        }
        public PartialViewResult OnGetCreate()
        {
           return Partial("./Create",new CreateSlider());
        }

      
        public JsonResult OnPostCreate(CreateSlider command)
        {
            var result = _sliderApplication.Create(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetEdit(long id)
        {
            var slider = _sliderApplication.GetDetails(id);
            return Partial("Edit", slider);
        }
        public JsonResult OnPostEdit(EditSlider command)
        {
            var result = _sliderApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result = _sliderApplication.Remove(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");

        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _sliderApplication.Restore(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");

        }

    }
}
