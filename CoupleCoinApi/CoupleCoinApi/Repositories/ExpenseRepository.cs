using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;

namespace CoupleCoinApi.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }

        public Expense CreateExpense(Expense expense)
        {
            try
            {
                _context.Expense.Add(expense);
                _context.SaveChanges();
                return expense;
            }
            catch 
            {
                return null;
            }
        }

        public bool CreateExpenseXOwner(ExpenseXOwner expenseXOwner)
        {
            try 
            {
                _context.ExpenseXOwner.Add(expenseXOwner);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            throw new NotImplementedException();
        }
    }
}
