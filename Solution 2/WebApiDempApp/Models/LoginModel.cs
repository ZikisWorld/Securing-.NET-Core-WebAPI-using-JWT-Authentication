using System.ComponentModel.DataAnnotations;

namespace WebApiDempApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserID is required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
