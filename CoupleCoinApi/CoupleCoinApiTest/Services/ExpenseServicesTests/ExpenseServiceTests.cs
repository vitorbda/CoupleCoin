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
        private readonly Mock<IExpenseTypeRepository> mockExpenseTypeRepository = new Mock<IExpenseTypeRepository>();
        private readonly ExpenseService _expenseService;
        private readonly Expense validExpense = new Expense
        {
            Id = 1,
            ExpenseValue = 10.10,
            ExpenseDate = DateTime.Now,
            IsActive = true
        };

        public ExpenseServiceTests()
        {
            _expenseService = new ExpenseService(mockCoupleRepository.Object, 
                mockUserRepository.Object,
                mockExpenseRepository.Object,
                mockExpenseTypeRepository.Object);
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
