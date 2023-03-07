using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Domain;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Slider;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SliderRepository : RepositoryBase<long, Slider>, ISliderRepository
    {
        private readonly ShopContext _shopContext;
        public SliderRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public EditSlider GetDetails(long id)
        {
            return _shopContext.Sliders.Select(
                x => new EditSlider
                {
                    Id = x.Id,
                 //   Picture = x.Picture,
                    Heading = x.Heading,
                    BtnText = x.BtnText,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                    Title = x.Title,
                    Link = x.Link
                    
                    
                }).FirstOrDefault(x=>x.Id==id);
        }

        public List<SliderViewModel> GetList()
        {
            return _shopContext.Sliders.Select(x => new SliderViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Heading = x.Heading,
                Picture = x.Picture,
                CreatonDate = x.CreationDate.ToFarsi(),
                IsRemoved = x.IsRemoved

            }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
