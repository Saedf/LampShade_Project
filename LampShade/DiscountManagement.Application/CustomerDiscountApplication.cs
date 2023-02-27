using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Create(DefineCustomerDiscount command)
        {
            var operation=new OperationResult();
          
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate=command.EndDate.ToGeorgianDateTime();
            if (_customerDiscountRepository.Exists(x =>  x.StartDate == startDate && x.EndDate==endDate))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }


            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate
                , endDate, command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation=new OperationResult();
            var customerDiscount = _customerDiscountRepository.GetBy(command.Id);
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            if (customerDiscount==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_customerDiscountRepository.Exists(x=>x.ProductId==command.ProductId &&
                                                      x.DiscountRate==command.DiscountRate &&
                                                      x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            customerDiscount.Edit(command.ProductId,command.DiscountRate,startDate,endDate,command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();

        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }
    }
}
