using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.CoupleServices;
using CoupleCoinApi.Services.CoupleServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupleCoinApiTest.Services.CoupleServicesTests
{
    public class CoupleServiceTests
    {
        #region Initialize
        private readonly Mock<IUserRepository> mockUser = new Mock<IUserRepository>();
        private readonly Mock<ICoupleRepository> mockCoupleRepository = new Mock<ICoupleRepository>();
        private readonly Mock<IUserService> mockUserService = new Mock<IUserService>();
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Couple validCouple = new Couple
        {
            IsActive = true,
            User1 = new User(),
            User2 = new User()
        };
        private readonly CoupleService _coupleService;
        private readonly User validUser = new User
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
        public CoupleServiceTests() 
        {
            _coupleService = new CoupleService(mockUser.Object, mockCoupleRepository.Object, mockUserService.Object);
        }
        #endregion

        #region ValidateUserToCouple method
        [Fact]
        public async void When_call_ValidateUserToCouple_method_with_inactive_OR_inexistent_user_return_FALSE()
        {
            string userNameInexistent = "inexistentUser";
            mockUser.Setup(_ => _.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());

            var userToCheck = await _coupleService.ValidateUserToCouple(userNameInexistent);

            Assert.False(userToCheck.Valid);
            Assert.Equal("Usuário para colaboração inválido!", userToCheck.Message);
            Assert.Equal(401, userToCheck.StatusCode);
        }

        [Fact]
        public async void When_call_ValidateUserToCouple_method_with_VALID_user_return_TRUE()
        {
            string userNameValid = "Test";
            mockUser.Setup(_ => _.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);

            var userToCheck = await _coupleService.ValidateUserToCouple(userNameValid);

            Assert.True(userToCheck.Valid);
        }
        #endregion

        #region VerifiyExistentCouple method
        [Fact]
        public async void When_call_VerifiyExistentCouple_method_with_existent_couple_return_FALSE()
        {
            string userName1 = "Test";
            string userName2 = "Test2";

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            mockUserService.Setup(x => x.VerifyIfUserIsActiveByUsername(It.IsAny<string>())).ReturnsAsync(true);

            var methodToTest = await _coupleService.VerifiyExistentCouple(userName1, userName2);

            Assert.True(methodToTest.Valid);
        }

        public async void When_call_VerifiyExistentCouple_method_with_not_existent_couple_return_TRUE()
        {

            var usernameTest1 = "Test";
            var usernameTwoTest1 = "Test";

            var usernameTest2 = "Test";
            var usernameTwoTest2 = "";

            var usernameTest3 = "";
            var usernameTwoTest3 = "Test";

            var usernameTest4 = "Test";
            var usernameTwoTest4 = "Test2";

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());

            var coupleToVerify1 = await _coupleService.VerifiyExistentCouple(usernameTest1, usernameTwoTest1);
            var coupleToVerify2 = await _coupleService.VerifiyExistentCouple(usernameTest2, usernameTwoTest2);
            var coupleToVerify3 = await _coupleService.VerifiyExistentCouple(usernameTest3, usernameTwoTest3);

            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            var coupleToVerify4 = await _coupleService.VerifiyExistentCouple(usernameTest4, usernameTwoTest4);

            Assert.False(coupleToVerify1.Valid);
            Assert.Equal("Necessário dois usuários para o vínculo", coupleToVerify1.Message);

            Assert.False(coupleToVerify2.Valid);
            Assert.Equal("Necessário dois usuários para o vínculo", coupleToVerify2.Message);

            Assert.False(coupleToVerify3.Valid);
            Assert.Equal("O segundo usuário não foi encontrado", coupleToVerify3.Message);

            Assert.False(coupleToVerify4.Valid);
            Assert.Equal("Vínculo de usuários não encontrado", coupleToVerify4.Message);
        }
        #endregion
    }
}
