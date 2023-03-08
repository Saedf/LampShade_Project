using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contract.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment comand)
        {
            var result=new OperationResult();
            var comment = new Comment(comand.Name, comand.Email, comand.Message, comand.ProductId);
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
