using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.ExpenseTypeServices.Interfaces
{
    public interface IExpenseTypeService
    {
        bool RegisterExpenseType(ExpenseTypeDTO ETD);
        ValidateRegisterModel VerifyExpenseType(int expanseTypeId, string username, string? usernametwo);
    }
}
