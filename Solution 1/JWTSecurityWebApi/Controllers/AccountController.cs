using JWTSecurityWebApi.Models;
using JWTSecurityWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTSecurityWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Authenticate")]
        public ActionResult Login(LoginModel loginModel)
        {
            User user = _authenticationService.Authenticate(loginModel);
            if (user == null)
                return Unauthorized("Invalid Username or Password");
            else
                return Ok(user);            
        }

        [Authorize]
        [HttpGet("SayHello")]        
        public ActionResult SayHello()
        {
             return Ok("Hello ");
            //return Ok("Hello " + name ?? string.Empty);
        }
    }
}
