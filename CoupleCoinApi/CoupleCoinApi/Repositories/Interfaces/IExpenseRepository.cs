using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        Expense CreateExpense(Expense expense);
        bool CreateExpenseXOwner(ExpenseXOwner expenseXOwner);
    }
}
