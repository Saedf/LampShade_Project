using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository:RepositoryBase<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly  ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts
                .Select(x=>new EditCustomerDiscount
                {
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    EndDate = x.EndDate.ToFarsi(),
                    StartDate = x.StartDate.ToFarsi(),
                    Id = x.Id,
                    Reason = x.Reason,

                })
                .FirstOrDefault(x=>x.Id==id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Name,x.Id}).ToList();
            var query = _discountContext.CustomerDiscounts
                .Select(x => new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    EndDate = x.EndDate.ToFarsi(),
                    EndDateGr = x.EndDate,
                    StartDateGr = x.StartDate,
                    StartDate = x.StartDate.ToFarsi(),
                    Reason = x.Reason,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());

            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount=>
                discount.Product=products.FirstOrDefault(x=>x.Id==discount.ProductId)?.Name);
            return discounts;

        }
    }
}
