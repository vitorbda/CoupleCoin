using CoupleCoinApi.Models;
using CoupleCoinApi.Models.ViewModel;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CoupleCoinApi.Controllers.AuthController;

namespace CoupleCoinApi.Services.AuthServices
{
    public class LoginService : ILoginService
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public LoginService(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        #endregion

        public async Task<UserViewModel> Login(LoginModel login)
        {
            var user = await ValidateUser(login);

            if (string.IsNullOrEmpty(user.UserName))
                return new UserViewModel();

            var userToReturn = ConvertUserService.ConvertUserToUserViewModel(user);

            userToReturn.Token = TokenService.GenerateToken(user);

            return userToReturn;
        }

        public async Task<User> ValidateUser(LoginModel login)
        {
            User voidUser = new User();

            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return voidUser;

            var user = _userRepository.GetActiveUserByUserName(login.UserName);

            var validatePassword = await _userService.VerifyPassword(login.Password, login.UserName);
            if (validatePassword)
                return user;

            return voidUser;
        }
    }
}
