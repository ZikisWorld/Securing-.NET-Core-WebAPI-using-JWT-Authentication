using JWTSecurityWebApi.Models;

namespace JWTSecurityWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>()
        {
            new User(){UserID = "zshaik", FirstName="Zikriya", LastName="Shaik", Role="Advisor", Password="test"},
            new User(){UserID = "jpati", FirstName="Jitendra", LastName="Pati", Role="Manager", Password="test"},
            new User(){UserID = "jsingh", FirstName="Jaspreet", LastName="Singh", Role="Tech Lead", Password="test"}
        };

        public User GetUser(string userId)
        {
            return _users.FirstOrDefault(x => x.UserID == userId);
        }

        public bool IsUserValid(LoginModel loginModel)
        {
            if(loginModel == null) return false;

            var user = GetUser(loginModel.UserID);
            if(user == null) return false;

            if (user.Password == loginModel.Password) return true;
            else return false;
        }
    }
}
