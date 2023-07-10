using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseServices;
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
        private ExpenseTypeDTO validETD = new ExpenseTypeDTO
        {
            Name = "Test",
            Owner = "Test",
            OwnerTwo = "Test"
        };

        public ExpenseServiceTests()
        {
            _expenseService = new ExpenseService(mockCoupleRepository.Object, 
                mockUserRepository.Object,
                mockExpenseRepository.Object);
        }
        #endregion

        #region VerifyCouple method
        [Fact]
        public async void When_call_VerifyCouple_method_with_a_valid_couple_return_TRUE()
        {
            var validETD = this.validETD;

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(validCouple);

            var methodToTest = await _expenseService.VerifyCouple(validETD);

            Assert.True(methodToTest.Valid);
        }

        [Fact]
        public async void When_call_VerifyCouple_method_with_a_invalid_couple_return_FALSE()
        {
            var ETDToTest1 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "Test",
            };

            var ETDToTest2 = new ExpenseTypeDTO
            {
                Name = "Test",
                OwnerTwo = "Test",
            };

            var ETDToTest3 = new ExpenseTypeDTO
            {
                Name = "Test",
                Owner = "Test",
                OwnerTwo = "Test"
            };

            mockCoupleRepository.Setup(_ => _.GetActiveCoupleByTwoUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(new Couple());

            var coupleToVerify1 = await _expenseService.VerifyCouple(ETDToTest1);
            var coupleToVerify2 = await _expenseService.VerifyCouple(ETDToTest2);
            var coupleToVerify3 = await _expenseService.VerifyCouple(ETDToTest3);

            Assert.False(coupleToVerify1.Valid);
            Assert.Equal("Necessário dois usuários para o vínculo", coupleToVerify1.Message);

            Assert.False(coupleToVerify2.Valid);
            Assert.Equal("Necessário dois usuários para o vínculo", coupleToVerify2.Message);

            Assert.False(coupleToVerify3.Valid);
            Assert.Equal("Vínculo de usuários não encontrado", coupleToVerify3.Message);
        }
        #endregion

        #region RegisterExpenseType 
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
    }
}
