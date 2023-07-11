using CoupleCoinApi.dbContext;
using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using System.Linq.Expressions;

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

        public ExpenseType GetActiveExpenseTypeById(int id)
        {
            var expenseType = _context.ExpenseType.FirstOrDefault(_ => _.Id == id && _.IsActive == true);
            return expenseType;
        }
    }
}
