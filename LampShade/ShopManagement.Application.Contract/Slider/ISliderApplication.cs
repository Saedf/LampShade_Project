using _01_Framework.Application;

namespace ShopManagement.Application.Contract.Slider;

public interface ISliderApplication
{
    OperationResult Create(CreateSlider command);
    OperationResult Edit(EditSlider command);
    List<SliderViewModel> GetList();
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditSlider GetDetails(long id);
}