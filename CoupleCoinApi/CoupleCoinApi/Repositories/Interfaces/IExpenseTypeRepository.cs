using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IExpenseTypeRepository
    {
        ExpenseType GetActiveExpenseTypeById(int id);
        bool CreateExpenseType(ExpenseType ET);
    }
}
