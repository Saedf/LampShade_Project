using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId &&
                    x.DiscountRate == command.DiscountRate))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var discount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(discount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(command.Id);
            if (colleagueDiscount == null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_colleagueDiscountRepository.Exists(x => x.DiscountRate == command.DiscountRate 
                    && x.ProductId == command.ProductId
                    && x.Id != command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            colleagueDiscount.Edit(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }

        public OperationResult Remove(long id)
        {
            var operation=new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.GetBy(id);
            if (colleagueDiscount == null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }
    }
}
