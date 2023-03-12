using _01_Framework.Application;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
        OperationResult Confirmed(long id);
        OperationResult Canceled(long id);
    }
}


