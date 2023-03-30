namespace _01_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        void SignIn(AuthViewModel account);
        string? CurrentAccountRole();
        AuthViewModel currentAccountInfo();
        List<int> GetPermissions();
        long CurrentAccountId();
    }
}
