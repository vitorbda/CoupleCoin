using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseTypeServices;
using CoupleCoinApi.Services.ExpenseTypeServices.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupleCoinApiTest.Services.ExpenseTypeServices
{
    public class ExpenseTypeServiceTests
    {
        private readonly ExpenseTypeDTO validETD = new ExpenseTypeDTO
        {
            Name = "Test",
            Owner = "Test",
            OwnerTwo = "Test"
        };
        private readonly ExpenseType validExpenseType = new ExpenseType
        {
            Id = 1,
            Name = "Test",
            AddDate = DateTime.Now,
            IsActive = true
        };
        private readonly Couple validCouple = new Couple
        {
            IsActive = true,
            User1 = new User(),
            User2 = new User()
        };
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
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<ICoupleRepository> mockCoupleRepository = new Mock<ICoupleRepository>();
        private readonly Mock<IExpenseTypeRepository> mockExpenseTypeRepository = new Mock<IExpenseTypeRepository>();
        private readonly ExpenseTypeService _expenseTypeService;

        public ExpenseTypeServiceTests()
        {
            _expenseTypeService = new ExpenseTypeService(mockExpenseTypeRepository.Object, mockCoupleRepository.Object, mockUserRepository.Object);
    }

        #region RegisterExpenseType method
        [Fact]
        public void When_call_RegisterExpenseType_method_with_a_valid_parameters_return_TRUE()
        {
            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);
            mockExpenseTypeRepository.Setup(_ => _.CreateExpenseType(It.IsAny<ExpenseType>())).Returns(true);

            var validETDWithCouple = this.validETD;
            var methodToVerifyWithCouple = _expenseTypeService.RegisterExpenseType(validETDWithCouple);

            var validETDWithoutCouple = this.validETD;
            validETDWithoutCouple.OwnerTwo = null;
            var methodToVerifyWithoutCouple = _expenseTypeService.RegisterExpenseType(validETDWithoutCouple);

            Assert.True(methodToVerifyWithCouple);

            Assert.True(methodToVerifyWithoutCouple);
        }
        #endregion

        #region VerifyExpenseType method
        [Fact]
        public void When_call_VerifyExpenseType_method_with_valid_parameters_return_TRUE()
        {
            var expanseTypeIdTest = 1;
            var usernameTest = "Test";
            var usernameTwoTest = "Test2";

            var validUser = this.validUser;
            var validCouple = this.validCouple;
            var validExpenseType = this.validExpenseType;

            mockExpenseTypeRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(validExpenseType);
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            mockCoupleRepository.Setup(x => x.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);

            validExpenseType.Owner = validUser;
            var testWithOneUser = _expenseTypeService.VerifyExpenseType(1, usernameTest);

            validExpenseType.Couple = validCouple;
            var testWithTwoUsers = _expenseTypeService.VerifyExpenseType(1, usernameTest, usernameTwoTest);

            Assert.True(testWithOneUser.Valid);
            Assert.True(testWithTwoUsers.Valid);
        }

        public void When_call_VerifyExpenseType_method_with_invalid_parameters_return_FALSE()
        {
            var expenseTypeIdTest1 = 1;
            var usernameTest1 = "Test";
            var usernameTwoTest1 = "Test2";
            var validExpenseType = this.validExpenseType;
            validExpenseType.Couple = validCouple;

            mockExpenseTypeRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(new ExpenseType());
            var testMethod_NotFound = _expenseTypeService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1, usernameTwoTest1);

            mockExpenseTypeRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(validExpenseType);
            mockCoupleRepository.Setup(x => x.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());
            var testMethod_CoupleUnauthorized = _expenseTypeService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1, usernameTwoTest1);

            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());
            var testeMethod_UserUnauthorized = _expenseTypeService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1);

            Assert.False(testMethod_NotFound.Valid);
            Assert.Equal("Tipo de despesa não encontrado", testMethod_NotFound.Message);
            Assert.Equal(404, testMethod_NotFound.StatusCode);

            Assert.False(testMethod_CoupleUnauthorized.Valid);
            Assert.Equal("Casal não autorizado [ExpenseType]", testMethod_CoupleUnauthorized.Message);
            Assert.Equal(401, testMethod_CoupleUnauthorized.StatusCode);

            Assert.False(testeMethod_UserUnauthorized.Valid);
            Assert.Equal("Usuário não autorizado [ExpenseType]", testeMethod_UserUnauthorized.Message);
            Assert.Equal(401, testeMethod_UserUnauthorized.StatusCode);
        }
        #endregion
    }
}
