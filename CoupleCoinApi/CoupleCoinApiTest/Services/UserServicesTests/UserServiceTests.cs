using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.UserServices;
using Moq;

namespace CoupleCoinApiTest.Services.UserServicesTests
{
    public class UserServiceTests
    {
        #region initialize
        private readonly User validUser = new User
        {
            Id = 1,
            Email = "test@teste.com",
            IsActive = true,
            LastName = "Test",
            Name = "Test",
            Password = "37-69-45-D7-41-3A-C6-3D-BD-62-F2-A4-0B-D7-1A-7F-19-49-DE-FA-BF-17-5C-72-86-81-8C-F1-D9-17-A7-56",
            Role = "Test",
            UserName = "Test"
        };
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService(mockUserRepository.Object);

            mockUserRepository.Setup(_ => _.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            mockUserRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(validUser);
        }
        #endregion

        #region ChangePassword method
        [Fact]
        public void When_call_ChangePassword_with_valid_parameters_returns_TRUE()
        {
            var newPassword = "senhaBoa#123";
            var username = "Test";
            
            mockUserRepository.Setup(_ => _.UpdateUser(It.IsAny<User>())).Returns(true);

            var passwordChanged = _userService.ChangePassword(newPassword, username);

            Assert.True(passwordChanged);
        }
        #endregion

        #region VerifyIfUserIsActiveByUsername method
        [Fact]
        public void When_call_VerifyIfUserIsActiveByUsername_with_valid_username_return_TRUE()
        {
            var username = "Test";

            var testMethod = _userService.VerifyIfUserIsActiveByUsername(username);

            Assert.True(testMethod);
        }

        [Fact]
        public void When_call_VerifyIfUserIsActiveByUsername_with_invalid_username_return_FALSE()
        {
            var usernameFail = "TestFail";
            var usernameFail2 = "";

            mockUserRepository.Setup(_ => _.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());

            var testMethod = _userService.VerifyIfUserIsActiveByUsername(usernameFail);
            var testMethod2 = _userService.VerifyIfUserIsActiveByUsername(usernameFail2);

            Assert.False(testMethod);
            Assert.False(testMethod2);
        }
        #endregion

        #region VerifyPassword method
        [Fact]
        public void When_call_VerifyPassword_method_with_valid_parameters_return_TRUE()
        {
            var username = "Test";
            var password = "senhaBoa#123";

            var testMethod = _userService.VerifyPassword(password, username);

            Assert.True(testMethod);
        }

        [Fact]
        public void When_call_VerifyPassword_method_with_invalid_parameters_return_FALSE()
        {
            var username = "Test";
            var voidUsername = "";
            var password = "123";

            var testMethod = _userService.VerifyPassword(password, voidUsername);
            var testMethod2 = _userService.VerifyPassword(password, username);

            Assert.False(testMethod);
            Assert.False(testMethod2);
        }

        #endregion

        #region VerifyEmail method
        [Fact]
        public void When_call_VerifyEmail_method_with_new_email_NOT_USED_return_TRUE()
        {
            var emailTest = "Test@test.com";

            mockUserRepository.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new User());

            var testMethod = _userService.VerifyEmail(emailTest);

            Assert.True(testMethod);
        }

        [Fact]
        public void When_call_VerifyEmail_method_with_new_email_IS_USE_return_FALSE()
        {
            var emailTest = "Test@test.com";

            var testMethod = _userService.VerifyEmail(emailTest);

            Assert.False(testMethod);
        }
        #endregion

        #region ChangeEmail method
        [Fact]
        public void When_call_ChangeEmail_method_with_valid_parameters_return_TRUE()
        {
            var username = "Test";
            var newEmail = "test@test.com";

            mockUserRepository.Setup(_ => _.UpdateUser(It.IsAny<User>())).Returns(true);

            var testMethod = _userService.ChangeEmail(newEmail, username);

            Assert.True(testMethod);
        }
        #endregion
    }
}
