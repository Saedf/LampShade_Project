﻿using _01_Framework.Infrastructure;
using CommentManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{

    public class CommentRepository: RepositoryBase<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                  WebSite = x.WebSite,
                    Message = x.Message,
                   OwnerRecordId = x.OwnerRecordId,
                    Type = x.Type,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    CommentDate = x.CreationDate.ToFarsi()
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
