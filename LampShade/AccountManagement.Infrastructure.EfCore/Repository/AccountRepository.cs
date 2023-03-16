using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository:RepositoryBase<long,Account>,IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Mobile = x.Mobile,

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts
                .Select(x=>new AccountViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,

                }).OrderByDescending(x=>x.Id).ToList();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts
                .Select(x => new AccountViewModel
            {
                    Id = x.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Mobile = x.Mobile,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    ProfilePhoto = x.ProfilePhoto,
                    RoleId = x.RoleId,
                    

            });
            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
            {
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
            {
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId>0)
            {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
