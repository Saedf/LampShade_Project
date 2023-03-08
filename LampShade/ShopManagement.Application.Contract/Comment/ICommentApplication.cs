using _01_Framework.Application;

namespace ShopManagement.Application.Contract.Comment;

public interface ICommentApplication
{
    OperationResult Add(AddComment comand);
    List<CommentViewModel> Search(CommentSearchModel searchModel);
    OperationResult Confirmed(long id);
    OperationResult Canceled(long id);
}