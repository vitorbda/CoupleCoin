using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;

namespace CoupleCoinApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUserName(string userName)
        {
            return _context.User.FirstOrDefault(_ => _.UserName == userName);
        }
    }
}
