using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository:RepositoryBase<long,ColleagueDiscount>,IColleagueDiscountRepository
    {

        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts
                .Select(x=>new ColleagueDiscountViewModel
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    ProductId = x.ProductId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    IsRemoved = x.IsRemoved
                    
                });
            if (searchModel.ProductId>0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            var discounts = query.OrderByDescending(x => x.Id)
                .ToList();
            discounts.ForEach(discount =>
                discount.Product=products.FirstOrDefault(x=>x.Id==discount.ProductId)?.Name);

            return discounts;

        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts
                .Select(x=>new EditColleagueDiscount
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    ProductId = x.ProductId,
                    
                })
                .FirstOrDefault(x=>x.Id==id);
        }
    }
}
