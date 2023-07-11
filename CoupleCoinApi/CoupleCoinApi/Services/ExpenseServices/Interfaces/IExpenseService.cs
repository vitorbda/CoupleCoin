using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.ExpenseServices.Interfaces
{
    public interface IExpenseService
    {
        Task<ValidateRegisterModel> VerifyCouple(string username, string usernametwo);
        bool RegisterExpenseType(ExpenseTypeDTO ETD);
        bool RegisterExpense(ExpenseDTO expense);
        ValidateRegisterModel VerifyExpenseType (int expanseTypeId, string username, string? usernametwo);
    }
}
