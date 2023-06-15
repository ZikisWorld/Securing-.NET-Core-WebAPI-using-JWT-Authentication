using JWTSecurityWebApi.Models;

namespace JWTSecurityWebApi.Repository
{
    public interface IUserRepository
    {
        User GetUser(string username);
        bool IsUserValid(LoginModel loginModel);
    }
}
