using _01_Framework.Application;

namespace DiscountManagement.Application.Contract.ColleagueDiscount;

public interface IColleagueDiscountApplication
{
    OperationResult Define(DefineColleagueDiscount command);
    OperationResult Edit(EditColleagueDiscount command);
    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
    EditColleagueDiscount GetDetails(long id);
}