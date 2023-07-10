using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.CoupleServices;
using CoupleCoinApi.Services.CoupleServices.Interfaces;
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
            _coupleService = new CoupleService(mockUser.Object, mockCoupleRepository.Object);
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

            var coupleExistent = new Couple { Id = 1 };

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(coupleExistent);

            var verifyCouple = await _coupleService.VerifiyExistentCouple(userName1, userName2);

            Assert.False(verifyCouple.Valid);
            Assert.Equal("Usuários já vinculados!", verifyCouple.Message);
        }

        public async void When_call_VerifiyExistentCouple_method_with_not_existent_couple_return_TRUE()
        {
            string userName1 = "Test";
            string UserName2 = "Test2";

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());

            var verifyCouple = await _coupleService.VerifiyExistentCouple(userName1, UserName2);

            Assert.True(verifyCouple.Valid);
        }
        #endregion
    }
}
