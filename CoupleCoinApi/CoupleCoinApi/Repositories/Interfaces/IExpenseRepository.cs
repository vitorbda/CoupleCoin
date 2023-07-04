using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        bool CreateExpenseType(ExpenseType ET);
    }
}
