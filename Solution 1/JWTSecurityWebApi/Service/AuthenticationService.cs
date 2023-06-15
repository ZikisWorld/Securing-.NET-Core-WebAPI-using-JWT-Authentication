using JWTSecurityWebApi.Models;
using JWTSecurityWebApi.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTSecurityWebApi.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public User Authenticate(LoginModel loginModel)
        {
            //Validate User credentials
            if (_userRepository.ValidateUser(loginModel)) 
            {
                //Valid Credentials. Get User Details
                User user = _userRepository.GetUser(loginModel.UserID);

                //Create JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Subject = Payload in JWT
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserID),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("aud", "aud1"), //Audience
                        new Claim("iss", "ZCompany.com")// Issuer
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    //SigningCredentials = Header in JWT. It contains Algorithm name, and Shared Secret Key
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);// Security Token 
                user.Token = tokenHandler.WriteToken(token); //Security Token converted to String Token
                user.Password = null;// Remove Pwd before returning
                return user;
            }
            else 
            { //Invalid User
                //TODO return NotAuthorized
                return null;
            }
        }
    }
}
