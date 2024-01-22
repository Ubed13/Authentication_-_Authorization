
using Role_Base_Authentication_JWT.Models;

namespace Role_Base_Authentication_JWT.Interface
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);
        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);
    }
}
