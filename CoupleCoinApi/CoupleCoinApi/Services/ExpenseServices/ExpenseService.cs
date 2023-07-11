using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using System;

namespace CoupleCoinApi.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        #region Constructor
        private readonly ICoupleRepository _coupleRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserService _userService;
        public ExpenseService(ICoupleRepository coupleRepository, IUserRepository userRepository, IExpenseRepository ExpenseRepository, IUserService userService)
        {
            _coupleRepository = coupleRepository;
            _userRepository = userRepository;
            _expenseRepository = ExpenseRepository;
            _userService = userService;
        }
        #endregion

        public bool RegisterExpense(ExpenseDTO expense)
        {
            var expenseType = _expenseRepository.GetActiveExpenseTypeById(expense.ExpenseTypeId);
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

        public bool RegisterExpenseType(ExpenseTypeDTO ETD)
        {
            var expenseTypeCreated = false;
            var expenseType = new ExpenseType
            {
                Name = ETD.Name,
                AddDate = DateTime.Now,
                IsActive = true
            };


            if (!string.IsNullOrEmpty(ETD.OwnerTwo))
            {
                var coupleToET = _coupleRepository.GetActiveCoupleByTwoUserName(ETD.OwnerTwo, ETD.Owner);

                if (coupleToET == null || string.IsNullOrEmpty(coupleToET.Id.ToString()))
                    return false;

                expenseType.Couple = coupleToET;
                expenseType.IsCouple = true;
            }
            else
            {
                var userOwner = _userRepository.GetActiveUserByUserName(ETD.Owner);

                expenseType.IsCouple = false;
                expenseType.Owner = userOwner;
            }

            expenseTypeCreated = _expenseRepository.CreateExpenseType(expenseType);

            return expenseTypeCreated;
        }

        public async Task<ValidateRegisterModel> VerifyCouple(string username, string usernametwo)
        {
            var valid = new ValidateRegisterModel { Valid = false };

            if (string.IsNullOrEmpty(usernametwo) || string.IsNullOrEmpty(username))
            {
                valid.Message = "Necessário dois usuários para o vínculo";
                return valid;
            }

            var verifyUser = await _userService.VerifyIfUserIsActiveByUsername(usernametwo);
            if (!verifyUser)
            {
                valid.Message = "O segundo usuário não foi encontrado";
                return valid;
            }

            var coupleToVerify = _coupleRepository.GetActiveCoupleByTwoUserName(username, usernametwo);
            if (coupleToVerify == null || coupleToVerify.User1 == null)
            {
                valid.Message = "Vínculo de usuários não encontrado";
                return valid;
            }

            valid.Valid = true;
            return valid;
        }

        public ValidateRegisterModel VerifyExpenseType(int expanseTypeId, string username, string usernametwo = "")
        {
            var valid = new ValidateRegisterModel { Valid = false };
            var expenseType = _expenseRepository.GetActiveExpenseTypeById(expanseTypeId);
            if (expenseType == null || string.IsNullOrEmpty(expenseType.Name))
            {
                valid.Message = "Tipo de despesa não encontrado";
                valid.StatusCode = 404;
                return valid;
            }

            if (!string.IsNullOrEmpty(usernametwo) && expenseType.Couple != null)
            {
                var couple = _coupleRepository.GetActiveCoupleByTwoUserName(username, usernametwo);

                if (expenseType.Couple.Equals(couple))
                {
                    valid.Valid = true;
                }
                else
                {
                    valid.Message = "Casal não autorizado [ExpenseType]";
                    valid.StatusCode = 401;
                }
            }
            else
            {
                var user = _userRepository.GetActiveUserByUserName(username);

                if (expenseType.Owner != null && expenseType.Owner.Equals(user))
                {
                    valid.Valid = true;
                }
                else
                {
                    valid.Message = "Usuário não autorizado [ExpenseType]";
                    valid.StatusCode = 401;
                }
            }

            return valid;
        }
    }
}
