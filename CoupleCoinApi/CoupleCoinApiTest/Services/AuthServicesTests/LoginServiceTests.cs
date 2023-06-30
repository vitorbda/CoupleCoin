using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices;
using Moq;

namespace CoupleCoinApiTest.Services.AuthServicesTests
{
    public class LoginServiceTests
    {
        #region Initialize
        private readonly LoginService _loginService;
        private readonly User user = new User
        {
            Id = 0,
            Email = "",
            IsActive = true,
            LastName = "Test",
            Name = "Test",
            Password = "53-2E-AA-BD-95-74-88-0D-BF-76-B9-B8-CC-00-83-2C-20-A6-EC-11-3D-68-22-99-55-0D-7A-6E-0F-34-5E-25",
            Role = "Test",
            UserName = "Test"
        };
        private readonly Mock<IUserRepository> mock = new Mock<IUserRepository>();
        public LoginServiceTests()
        {
            _loginService = new LoginService(mock.Object);
            mock.Setup(_ => _.GetUserByUserName(It.IsAny<string>())).Returns(user);
        }
        #endregion

        [Fact]
        public void When_calling_login_method_SHOULD_return_the_token()
        {
            LoginModel lmTest = new LoginModel { Password = "Test", UserName = "Test" };

            var token = _loginService.Login(lmTest);

            Assert.False(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void When_calling_login_method_MUST_NOT_return_the_token()
        {
            LoginModel lmTest = new LoginModel { Password = "Test1", UserName = "Test" };

            var token = _loginService.Login(lmTest);

            Assert.True(string.IsNullOrEmpty(token));
        }

        [Fact]
        public void When_calling_ValidateUser_method_SHOULD_return_user()
        {
            var lmTest = new LoginModel { UserName = "Test", Password = "Test" };

            var userTest = _loginService.ValidateUser(lmTest);

            Assert.Equal(user, userTest);
        }

        [Fact]
        public void When_calling_ValidateUser_method_MUST_NOT_return_user()
        {
            var lmTest = new LoginModel { UserName = "faiulure", Password = "faiulure" };
            var lmTest2 = new LoginModel { UserName = "Test" };
            var lmTest3 = new LoginModel { Password = "Test" };
            var lmTest4 = new LoginModel();

            var userTest = _loginService.ValidateUser(lmTest);
            var userTest2 = _loginService.ValidateUser(lmTest2);
            var userTest3 = _loginService.ValidateUser(lmTest3);
            var userTest4 = _loginService.ValidateUser(lmTest4);

            Assert.NotEqual(user, userTest);
            Assert.NotEqual(user, userTest2);
            Assert.NotEqual(user, userTest3);
            Assert.NotEqual(user, userTest4);
        }
    }
}