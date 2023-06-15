using JWTSecurityWebApi.Models;

namespace JWTSecurityWebApi.Service
{
    public interface IAuthenticationService
    {
        User Authenticate(LoginModel loginModel);
    }
}
