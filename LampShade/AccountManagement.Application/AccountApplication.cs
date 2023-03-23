using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _repository;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IRoleRepository roleRepository, IAuthHelper authHelper, IRoleRepository repository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _roleRepository = roleRepository;
            _authHelper = authHelper;
            _repository = repository;
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation=new OperationResult();

            if (_accountRepository.Exists(x=>x.UserName==command.UserName
                ||x.Mobile==command.Mobile))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }

            var passwrod = _passwordHasher.Hash(command.Password);
            var path = $"profilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            var account = new Account(command.FullName, command.UserName, passwrod, command.Mobile, picturePath,command.RoleId);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();


        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (_accountRepository.Exists(x => (x.UserName == command.UserName
                                               || x.Mobile == command.Mobile)&& x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            var path = $"profilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.FullName,command.UserName,command.Mobile,picturePath,command.RoleId);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Id);
            if (account==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }

            if (command.Password!=command.RePassword)
            {
                return operation.Faild(ApplicationMessage.PasswordDosntMatch);
            }

            var passwordHashed = _passwordHasher.Hash(command.Password);
            account.ChangePassword(passwordHashed);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Login(Login command)
        {
            var operation=new OperationResult();
           var account= _accountRepository.GetBy(command.UserName);
            if (account==null)
            {
                return operation.Faild(ApplicationMessage.WrongUserPass);
            }

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
            {
                return operation.Faild(ApplicationMessage.WrongUserPass);
            }

            var permissions = _repository.GetBy(account.RoleId)
                .Permissions
                .Select(x => x.Code)
                .ToList();
            var authViewModel=new AuthViewModel(account.Id,account.RoleId,account.FullName,account.UserName
            ,account.Mobile,permissions);
            _authHelper.SignIn(authViewModel);

            return operation.Succeeded();
        }

        public List<AccountViewModel> Search(AccountSearchModel search)
        {
            return _accountRepository.Search(search);
        }

        public AccountViewModel GetAccountBy(long id)
        {
            throw new NotImplementedException();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
