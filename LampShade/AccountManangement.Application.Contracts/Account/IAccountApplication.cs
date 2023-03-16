using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public interface IAccountApplication
{
    OperationResult Register(RegisterAccount command);
    OperationResult Edit(EditAccount command);
    OperationResult ChangePassword(ChangePassword command);
    OperationResult Login(Login command);
    List<AccountViewModel> Search(AccountSearchModel search);
    AccountViewModel GetAccountBy(long id);
    EditAccount GetDetails(long id);
    void Logout();
    List<AccountViewModel> GetAccounts();

}