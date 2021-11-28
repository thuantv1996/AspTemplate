using Global.DTO;

namespace BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        AddUserResult AddUser(UserDTO userRegister);
        LoginResult Login(UserDTO userLogin);
    }
}
