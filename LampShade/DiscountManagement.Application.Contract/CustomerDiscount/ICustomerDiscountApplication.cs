using _01_Framework.Application;

namespace DiscountManagement.Application.Contract.CustomerDiscount;

public interface ICustomerDiscountApplication
{
    OperationResult Create(DefineCustomerDiscount command);
    OperationResult Edit(EditCustomerDiscount command);
    List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    EditCustomerDiscount GetDetails(long id);
}