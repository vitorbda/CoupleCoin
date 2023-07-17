using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.ExpenseServices.Interfaces
{
    public interface IExpenseService
    {
        bool RegisterExpense(ExpenseDTO expense);
    }
}
