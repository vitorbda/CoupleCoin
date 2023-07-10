using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseServices.Interfaces;
using System;

namespace CoupleCoinApi.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        #region Constructor
        private readonly ICoupleRepository _coupleRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseService(ICoupleRepository coupleRepository, IUserRepository userRepository, IExpenseRepository ExpenseRepository)
        {
            _coupleRepository = coupleRepository;
            _userRepository = userRepository;
            _expenseRepository = ExpenseRepository;
        }
        #endregion

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

        public async Task<ValidateRegisterModel> VerifyCouple(ExpenseTypeDTO ETD)
        {
            var valid = new ValidateRegisterModel { Valid = false };

            if (string.IsNullOrEmpty(ETD.OwnerTwo) || string.IsNullOrEmpty(ETD.Owner))
            {
                valid.Message = "Necessário dois usuários para o vínculo";
                return valid;
            }

            var coupleToVerify = _coupleRepository.GetActiveCoupleByTwoUserName(ETD.Owner, ETD.OwnerTwo);
            if (coupleToVerify == null || coupleToVerify.User1 == null)
            {
                valid.Message = "Vínculo de usuários não encontrado";
                return valid;
            }

            valid.Valid = true;
            return valid;
        }

    }
}
