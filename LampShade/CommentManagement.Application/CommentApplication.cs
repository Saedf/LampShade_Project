using _01_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var result=new OperationResult();
            var comment = new Comment(command.Name,command.Email, command.WebSite,command.Message
            ,command.OwnerRecordId,command.Type,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return result.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }

        public OperationResult Confirmed(long id)
        {
            var operation=new OperationResult();
            var comment = _commentRepository.GetBy(id);
            if (comment==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            comment.Confirmed();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Canceled(long id)
        {
            var operation=new OperationResult();
            var comment=_commentRepository.GetBy(id);
            if (comment==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            comment.Canceled();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
