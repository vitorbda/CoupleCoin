using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/account")]
    public class AuthController : Controller
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;
        #endregion
                
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginModel login)
        {
            User user = ValidateUser(login);

            if (string.IsNullOrEmpty(user.UserName)) 
                return Unauthorized(new { message = "Usuário ou senha inválidos!" });

            var token = TokenService.GenerateToken(user);

            UserModel userModel = ConvertUserToUserModel(user);

            return Ok(new { token = token });
        }

        private User ValidateUser(LoginModel login)
        {
            User voidUser = new User();

            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return voidUser;

            var user = _userRepository.GetUserByUserName(login.UserName);

            if (user == null || user.IsActive == false)
                return voidUser;

            string hashPassword = EncryptService.ConvertToSHA256Hash(login.Password);

            if (user.Password == hashPassword)
                return user;

            return voidUser;
        }

        private UserModel ConvertUserToUserModel(User user)
        {
            try
            {
                return new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = user.Role
                };                
            }
            catch
            {
                return null;
            }
        }

        #region Models
        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UserModel
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }
        }
        #endregion
    }
}
