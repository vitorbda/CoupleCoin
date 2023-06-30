using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;

namespace CoupleCoinApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Constructor
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        public bool CreateUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch 
            { 
                return false;
            }            
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(_ => _.Email == email);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.User.FirstOrDefault(_ => _.UserName == userName);
        }

        public User GetActiveUserByUserName(string userName)
        {
            return _context.User.Where(_ => _.UserName == userName && _.IsActive == true).FirstOrDefault();
                                
        }
    }
}
