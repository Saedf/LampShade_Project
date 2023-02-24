using _01_Framework.Domain;
using ShopManagement.Application.Contract.Slider;

namespace ShopManagement.Domain.SliderAgg;

public interface ISliderRepository:IRepository<long,Slider>
{
    EditSlider GetDetails(long id);
    List<SliderViewModel> GetList();
}