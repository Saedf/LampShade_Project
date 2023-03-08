using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly ShopContext _context;
        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    Id =x.Id,
                    Email = x.Email,
                    Message = x.Message,
                    ProductId = x.ProductId,
                    ProdcutName = x.Product.Name,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    CommentDate = x.CreationDate.ToFarsi(),
                    Name = x.Name,
                });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x=>x.Name.Contains(searchModel.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
