using _01_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg;

public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
{
    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    EditColleagueDiscount GetDetails(long id);
}