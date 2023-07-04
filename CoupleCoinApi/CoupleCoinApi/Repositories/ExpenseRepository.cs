using CoupleCoinApi.dbContext;
using CoupleCoinApi.DTO;
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
    }
}
