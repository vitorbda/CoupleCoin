using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseServices;
using CoupleCoinApi.Services.UserServices;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupleCoinApiTest.Services.ExpenseServicesTests
{
    public class ExpenseServiceTests
    {
        #region Initialize
        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IExpenseRepository> mockExpenseRepository = new Mock<IExpenseRepository>();
        private readonly Mock<ICoupleRepository> mockCoupleRepository = new Mock<ICoupleRepository>();
        private readonly ExpenseService _expenseService;
        private readonly UserService _userService;
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
        private readonly Couple validCouple = new Couple
        {
            IsActive = true,
            User1 = new User(),
            User2 = new User()
        };
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
        private readonly Expense validExpense = new Expense
        {
            Id = 1,
            ExpenseValue = 10.10,
            ExpenseDate = DateTime.Now,
            IsActive = true
        };

        public ExpenseServiceTests()
        {
            _userService = new UserService(mockUserRepository.Object);
            _expenseService = new ExpenseService(mockCoupleRepository.Object, 
                mockUserRepository.Object,
                mockExpenseRepository.Object,
                _userService);
        }
        #endregion

        #region VerifyCouple method
        [Fact]
        public async void When_call_VerifyCouple_method_with_a_valid_couple_return_TRUE()
        {
            var validETD = this.validETD;

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);

            var methodToTest = await _expenseService.VerifyCouple(validETD.Owner, validETD.OwnerTwo);

            Assert.True(methodToTest.Valid);
        }

        [Fact]
        public async void When_call_VerifyCouple_method_with_a_invalid_couple_return_FALSE()
        {
            var ETDToTest1 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "Test",
                OwnerTwo = ""
            };

            var ETDToTest2 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "",
                OwnerTwo = "Test",
            };

            var ETDToTest3 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "Test",
                OwnerTwo = "Test2"
            };

            var ETDToTest4 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "Test",
                OwnerTwo = "Test"
            };

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());

            var coupleToVerify1 = await _expenseService.VerifyCouple(ETDToTest1.Owner, ETDToTest1.OwnerTwo);
            var coupleToVerify2 = await _expenseService.VerifyCouple(ETDToTest2.Owner, ETDToTest2.OwnerTwo);
            var coupleToVerify3 = await _expenseService.VerifyCouple(ETDToTest3.Owner, ETDToTest3.OwnerTwo);

            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            var coupleToVerify4 = await _expenseService.VerifyCouple(ETDToTest3.Owner, ETDToTest3.OwnerTwo);

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

        #region RegisterExpenseType method
        [Fact]
        public void When_call_RegisterExpenseType_method_with_a_valid_parameters_return_TRUE()
        {
            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);
            mockExpenseRepository.Setup(_ => _.CreateExpenseType(It.IsAny<ExpenseType>())).Returns(true);

            var validETDWithCouple = this.validETD;
            var methodToVerifyWithCouple = _expenseService.RegisterExpenseType(validETDWithCouple);

            var validETDWithoutCouple = this.validETD;
            validETDWithoutCouple.OwnerTwo = null;
            var methodToVerifyWithoutCouple = _expenseService.RegisterExpenseType(validETDWithoutCouple);

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

            mockExpenseRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(validExpenseType);
            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(validUser);
            mockCoupleRepository.Setup(x => x.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);

            validExpenseType.Owner = validUser;
            var testWithOneUser = _expenseService.VerifyExpenseType(1,usernameTest);

            validExpenseType.Couple = validCouple;
            var testWithTwoUsers = _expenseService.VerifyExpenseType(1, usernameTest, usernameTwoTest);

            Assert.True(testWithOneUser.Valid);
            Assert.True(testWithTwoUsers.Valid);
        }

        [Fact]
        public void When_call_VerifyExpenseType_method_with_invalid_parameters_return_FALSE()
        {
            var expenseTypeIdTest1 = 1;
            var usernameTest1 = "Test";
            var usernameTwoTest1 = "Test2";
            var validExpenseType = this.validExpenseType;
            validExpenseType.Couple = validCouple;

            mockExpenseRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(new ExpenseType());
            var testMethod_NotFound = _expenseService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1, usernameTwoTest1);

            mockExpenseRepository.Setup(x => x.GetActiveExpenseTypeById(It.IsAny<int>())).Returns(validExpenseType);
            mockCoupleRepository.Setup(x => x.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());
            var testMethod_CoupleUnauthorized = _expenseService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1, usernameTwoTest1);

            mockUserRepository.Setup(x => x.GetActiveUserByUserName(It.IsAny<string>())).Returns(new User());
            var testeMethod_UserUnauthorized = _expenseService.VerifyExpenseType(expenseTypeIdTest1, usernameTest1);

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

        #region RegisterExpense method
        [Fact]
        public void When_call_RegisterExpense_with_valid_parameters_return_TRUE()
        {
            var expenseTestWithCouple = new ExpenseDTO
            {
                ExpenseValue = 10.10,
                UsernameOne = "Test",
                UsernameTwo = "Test2",
                Description = "Test",
                ExpenseDate = DateTime.Now,
                ExpenseTypeId = 1
            };

            var expenseTestWithoutCouple = new ExpenseDTO
            {
                ExpenseValue = 10.10,
                UsernameOne = "Test",
                Description = "Test",
                ExpenseDate = DateTime.Now,
                ExpenseTypeId = 1
            };

            var validExpense = this.validExpense;

            mockExpenseRepository.Setup(x => x.CreateExpense(It.IsAny<Expense>())).Returns(validExpense);
            mockExpenseRepository.Setup(x => x.CreateExpenseXOwner(It.IsAny<ExpenseXOwner>())).Returns(true);

            var testMethod_WithCouple = _expenseService.RegisterExpense(expenseTestWithCouple);
            var testMethod_WithoutCouple = _expenseService.RegisterExpense(expenseTestWithoutCouple);

            Assert.True(testMethod_WithCouple);
            Assert.True(testMethod_WithoutCouple);

        }
        #endregion
    }
}
