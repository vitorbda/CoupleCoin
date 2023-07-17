using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;

namespace CoupleCoinApi.Repositories
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly AppDbContext _context;
        public ExpenseTypeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public bool CreateExpenseType(ExpenseType ET)
        {
            try
            {
                _context.ExpenseType.Add(ET);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ExpenseType GetActiveExpenseTypeById(int id)
        {
            var expenseType = _context.ExpenseType.FirstOrDefault(_ => _.Id == id && _.IsActive == true);
            return expenseType;
        }
    }
}
