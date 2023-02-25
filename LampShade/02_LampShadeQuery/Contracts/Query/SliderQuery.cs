using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.Slider;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _02_LampShadeQuery.Contracts.Query
{
    public class SliderQuery:ISliderQuery
    {
        private  readonly ShopContext _shopContext;

        public SliderQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SliderQueryModel> GetDetails()
        {
            return _shopContext.Sliders
                .Where(x=>x.IsRemoved==false)
                .Select(x=> new SliderQueryModel
                {
                    Picture = x.Picture,
                    Heading = x.Heading,
                    BtnText = x.BtnText,
                    Text = x.Text,
                    Link = x.Link,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Title = x.Title
                }).AsNoTracking()
                .ToList();
        }
    }
}
