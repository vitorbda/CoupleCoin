using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.ExpenseTypeServices.Interfaces;

namespace CoupleCoinApi.Services.ExpenseTypeServices
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        #region Constructor
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly ICoupleRepository _coupleRepository;
        private readonly IUserRepository _userRepository;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, ICoupleRepository coupleRepository, IUserRepository userRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _coupleRepository = coupleRepository;
            _userRepository = userRepository;
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

            expenseTypeCreated = _expenseTypeRepository.CreateExpenseType(expenseType);

            return expenseTypeCreated;
        }

        public ValidateRegisterModel VerifyExpenseType(int expanseTypeId, string username, string usernametwo = "")
        {
            var valid = new ValidateRegisterModel { Valid = false };
            var expenseType = _expenseTypeRepository.GetActiveExpenseTypeById(expanseTypeId);
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
