using CoupleCoinApi.Models;
using CoupleCoinApi.Models.ViewModel;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices;
using CoupleCoinApi.Services.UserServices;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Moq;

namespace CoupleCoinApiTest.Services.AuthServicesTests
{
    public class LoginServiceTests
    {
        #region Initialize
        private readonly LoginService _loginService;
        private readonly UserService _userService;
        private readonly User user = new User
        {
            Id = 0,
            Email = "email@email.com",
            IsActive = true,
            LastName = "Test",
            Name = "Test",
            Password = "53-2E-AA-BD-95-74-88-0D-BF-76-B9-B8-CC-00-83-2C-20-A6-EC-11-3D-68-22-99-55-0D-7A-6E-0F-34-5E-25",
            Role = "Test",
            UserName = "Test"            
        };
        private readonly UserViewModel userVM = new UserViewModel { UserName = "Test",
                                                                    Name = "Test",
                                                                    Email = "email@email.com",
                                                                    LastName = "Test"};
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IUserService> mockUserService = new Mock<IUserService>();
        public LoginServiceTests()
        {
            _userService = new UserService(mockUserRepository.Object);
            _loginService = new LoginService(mockUserRepository.Object, _userService);
            mockUserRepository.Setup(_ => _.GetActiveUserByUserName(It.IsAny<string>())).Returns(user);
        }
        #endregion

        [Fact]
        public async void When_calling_login_method_with_correct_data_SHOULD_return_the_token()
        {
            LoginModel lmTest = new LoginModel { Password = "Test", UserName = "Test" };

            var userVMToReturn = await _loginService.Login(lmTest);

            Assert.Equal(userVM.UserName, userVMToReturn.UserName);
            Assert.Equal(userVM.Name, userVMToReturn.Name);
            Assert.Equal(userVM.LastName, userVMToReturn.LastName);
            Assert.Equal(userVM.Email, userVMToReturn.Email);
            Assert.True(!string.IsNullOrEmpty(userVMToReturn.Token));
        }

        [Fact]
        public async void When_calling_login_method_without_correct_data_MUST_NOT_return_the_token()
        {
            LoginModel lmTest = new LoginModel { Password = "Test1", UserName = "Test" };

            var userVMToReturn = await _loginService.Login(lmTest);

            Assert.True(string.IsNullOrEmpty(userVMToReturn.UserName));
            Assert.True(string.IsNullOrEmpty(userVMToReturn.Token));
        }

        [Fact]
        public async Task When_calling_ValidateUser_method_SHOULD_return_userAsync()
        {
            var lmTest = new LoginModel { UserName = "Test", Password = "Test" };

            var userTest = await _loginService.ValidateUser(lmTest);

            Assert.Equal(user, userTest);
        }

        [Fact]
        public async void When_calling_ValidateUser_method_MUST_NOT_return_user()
        {
            var lmTest = new LoginModel { UserName = "faiulure", Password = "faiulure" };
            var lmTest2 = new LoginModel { UserName = "Test" };
            var lmTest3 = new LoginModel { Password = "Test" };
            var lmTest4 = new LoginModel();

            var userTest = await _loginService.ValidateUser(lmTest);
            var userTest2 = await _loginService.ValidateUser(lmTest2);
            var userTest3 = await _loginService.ValidateUser(lmTest3);
            var userTest4 = await _loginService.ValidateUser(lmTest4);

            Assert.NotEqual(user, userTest);
            Assert.NotEqual(user, userTest2);
            Assert.NotEqual(user, userTest3);
            Assert.NotEqual(user, userTest4);
        }
    }
}