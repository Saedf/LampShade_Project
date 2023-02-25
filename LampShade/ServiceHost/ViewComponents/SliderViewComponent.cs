using _02_LampShadeQuery.Contracts.Slider;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ISliderQuery _sliderQuery;

        public SliderViewComponent(ISliderQuery sliderQuery)
        {
            _sliderQuery = sliderQuery;
        }

        public IViewComponentResult Invoke()
        {
            var sliders = _sliderQuery.GetDetails();
            return View(sliders);
        }
    }
}
