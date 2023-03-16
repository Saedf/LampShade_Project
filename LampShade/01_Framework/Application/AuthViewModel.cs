namespace _01_Framework.Application;

public class AuthViewModel
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public string Role { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Mobile { get; set; }

    public AuthViewModel()
    {
    }

    public AuthViewModel(long id, long roleId, string fullName, string userName, string mobile)
    {
        Id = id;
        RoleId = roleId;
        FullName = fullName;
        UserName = userName;
        Mobile = mobile;
    }
}