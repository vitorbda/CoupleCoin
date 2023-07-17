using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;

namespace CoupleCoinApi.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        #region Constructor
        private readonly ICoupleRepository _coupleRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        public ExpenseService(ICoupleRepository coupleRepository, IUserRepository userRepository, IExpenseRepository ExpenseRepository, IExpenseTypeRepository expenseTypeRepository)
        {
            _coupleRepository = coupleRepository;
            _userRepository = userRepository;
            _expenseRepository = ExpenseRepository;
            _expenseTypeRepository = expenseTypeRepository;
        }
        #endregion

        public bool RegisterExpense(ExpenseDTO expense)
        {
            var expenseType = _expenseTypeRepository.GetActiveExpenseTypeById(expense.ExpenseTypeId);
            var newExpanse = new Expense
            {
                ExpenseValue = expense.ExpenseValue,
                Description = expense.Description,
                ExpenseDate = expense.ExpenseDate,
                Type = expenseType
            };

            var expenseRegistered = _expenseRepository.CreateExpense(newExpanse);
            if (expenseRegistered == null)
                return false;

            var ExpenseXOwner = new ExpenseXOwner { Expense =  expenseRegistered };

            if (!string.IsNullOrEmpty(expense.UsernameTwo))
            {
                var couple = _coupleRepository.GetActiveCoupleByTwoUserName(expense.UsernameOne, expense.UsernameTwo);
                ExpenseXOwner.Couple = couple;
            }
            else
            {
                var user = _userRepository.GetActiveUserByUserName(expense.UsernameOne);
                ExpenseXOwner.User = user;
            }

            var expenseXOwnerCreated = _expenseRepository.CreateExpenseXOwner(ExpenseXOwner);

            return expenseXOwnerCreated;
        }        
    }
}
