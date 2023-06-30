using CoupleCoinApi.Models;
using CoupleCoinApi.Models.ViewModel;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CoupleCoinApi.Controllers.AuthController;

namespace CoupleCoinApi.Services.AuthServices
{
    public class LoginService : ILoginService
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public UserViewModel Login(LoginModel login)
        {
            var user = ValidateUser(login);

            if (string.IsNullOrEmpty(user.UserName))
                return new UserViewModel();

            var userToReturn = ConvertUserService.ConvertUserToUserViewModel(user);

            userToReturn.Token = TokenService.GenerateToken(user);

            return userToReturn;
        }

        public User ValidateUser(LoginModel login)
        {
            User voidUser = new User();

            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return voidUser;

            var user = _userRepository.GetActiveUserByUserName(login.UserName);

            if (user == null || user.IsActive == false)
                return voidUser;

            string hashPassword = EncryptService.ConvertToSHA256Hash(login.Password);

            if (user.Password == hashPassword)
                return user;

            return voidUser;
        }
    }
}
