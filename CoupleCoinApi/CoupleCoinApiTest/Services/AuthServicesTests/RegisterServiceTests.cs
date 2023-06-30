using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices;
using Moq;

namespace CoupleCoinApiTest.Services.AuthServicesTests
{
    public class RegisterServiceTests
    {
        #region Constructor
        private readonly RegisterService _registerService;
        private readonly Mock<IUserRepository> mock = new Mock<IUserRepository>();
        private readonly User user = new User
        {
            Id = 0,
            Email = "",
            IsActive = true,
            LastName = "Test",
            Name = "Test",
            Password = "Test",
            Role = "Test",
            UserName = "Test"
        };

        public RegisterServiceTests()
        {
            _registerService = new RegisterService(mock.Object);
            mock.Setup(_ => _.CreateUser(It.IsAny<User>())).Returns(true);
            mock.Setup(_ => _.GetUserByUserName(It.IsAny<string>())).Returns(user);
            mock.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new User());
        }
        #endregion

        #region RegisterUser Method
        [Fact]
        public void When_calling_the_RegisterUser_method_MUST_return_true()
        {
            var testUser = user;
            var sucessfullyRegister = _registerService.RegisterUser(testUser);

            Assert.True(sucessfullyRegister);
        }

        [Fact]
        public void When_calling_the_RegisterUser_method_with_null_user_MUST_return_FALSE()
        {
            var testUser = new User();

            var failedRegister = _registerService.RegisterUser(null);
            var failedRegister2 = _registerService.RegisterUser(testUser);

            Assert.False(failedRegister);
            Assert.False(failedRegister2);
        }
        #endregion

        #region ValidatePassword Method
        [Fact]
        public void When_calling_the_ValidatePassword_method_MUST_return_TRUE()
        {
            string goodPassword = "SenhaForte1!";
            string goodPassword2 = "2Abcd$ef";
            string goodPassword3 = "Strong#P4ss";

            var testPassword = _registerService.ValidatePassword(goodPassword).Valid;
            var testPassword2 = _registerService.ValidatePassword(goodPassword2).Valid;
            var testPassword3 = _registerService.ValidatePassword(goodPassword3).Valid;

            Assert.True(testPassword);
            Assert.True(testPassword2);
            Assert.True(testPassword3);
        }

        [Fact]
        public void When_calling_the_ValidatePassword_method_MUST_return_FALSE()
        {
            string badPassword = "senha";
            var badTest = new ValidateRegisterModel { Valid = false, Message = "Senha fraca! Mínimo de 8 caracteres" };

            string badPassword2 = "senha123";
            var badTest2 = new ValidateRegisterModel { Valid = false, Message = "Senha fraca! Necessário ao menos uma letra maiúscula" };

            string badPassword3 = "SENHA123";
            var badTest3 = new ValidateRegisterModel { Valid = false, Message = "Senha fraca! Necessário ao menos uma letra minúscula" };

            string badPassword4 = "Abcdefgh";
            var badTest4 = new ValidateRegisterModel { Valid = false, Message = "Senha fraca! Necessário ao menos um símbolo" };

            string badPassword5 = "Ab!@#$%^&*";
            var badTest5 = new ValidateRegisterModel { Valid = false, Message = "Senha fraca! Necessário ao menos um número" };

            string badPassword6 = "";
            string badPassword7 = null;
            var badTest6and7 = new ValidateRegisterModel { Valid = false, Message = "Senha vazia!" };

            var testPassword = _registerService.ValidatePassword(badPassword);
            var testPassword2 = _registerService.ValidatePassword(badPassword2);
            var testPassword3 = _registerService.ValidatePassword(badPassword3);
            var testPassword4 = _registerService.ValidatePassword(badPassword4);
            var testPassword5 = _registerService.ValidatePassword(badPassword5);
            var testPassword6 = _registerService.ValidatePassword(badPassword6);
            var testPassword7 = _registerService.ValidatePassword(badPassword7);

            Assert.Equal(badTest.Valid, testPassword.Valid);
            Assert.Equal(badTest.StatusCode, testPassword.StatusCode);
            Assert.Equal(badTest.Message, testPassword.Message);

            Assert.Equal(badTest2.Valid, testPassword2.Valid);
            Assert.Equal(badTest2.StatusCode, testPassword2.StatusCode);
            Assert.Equal(badTest2.Message, testPassword2.Message);

            Assert.Equal(badTest3.Valid, testPassword3.Valid);
            Assert.Equal(badTest3.StatusCode, testPassword3.StatusCode);
            Assert.Equal(badTest3.Message, testPassword3.Message);

            Assert.Equal(badTest4.Valid, testPassword4.Valid);
            Assert.Equal(badTest4.StatusCode, testPassword4.StatusCode);
            Assert.Equal(badTest4.Message, testPassword4.Message);

            Assert.Equal(badTest5.Valid, testPassword5.Valid);
            Assert.Equal(badTest5.StatusCode, testPassword5.StatusCode);
            Assert.Equal(badTest5.Message, testPassword5.Message);

            Assert.Equal(badTest6and7.Valid, testPassword6.Valid);
            Assert.Equal(badTest6and7.StatusCode, testPassword6.StatusCode);
            Assert.Equal(badTest6and7.Message, testPassword6.Message);

            Assert.Equal(badTest6and7.Valid, testPassword7.Valid);
            Assert.Equal(badTest6and7.StatusCode, testPassword7.StatusCode);
            Assert.Equal(badTest6and7.Message, testPassword7.Message);
        }
        #endregion

        #region ValidateUser Method
        [Fact]
        public void When_calling_the_ValidateUser_method_MUST_return_TRUE()
        {
            mock.Setup(_ => _.GetUserByUserName(It.IsAny<string>())).Returns(new User());
            mock.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(new User());

            var testUser = user;
            testUser.Password = "Adff1@#$%$";

            var testReturn = _registerService.ValidateUser(testUser);

            Assert.True(testReturn.Valid);
        }

        public void When_calling_the_ValidateUser_method_MUST_return_FALSE()
        {
            mock.Setup(_ => _.GetUserByUserName(It.IsAny<string>())).Returns(user);
            mock.Setup(_ => _.GetUserByEmail(It.IsAny<string>())).Returns(user);

            var testUser = user;

            var testReturn = _registerService.ValidateUser(testUser);

            Assert.False(testReturn.Valid);
        }
        #endregion
    }
}
