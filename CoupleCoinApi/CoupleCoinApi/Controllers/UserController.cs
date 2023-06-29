using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;

namespace CoupleCoinApi.Controllers
{
    public class UserController
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUserName(string name)
        {
            throw new NotImplementedException();
        }

        public User[] GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool InactiveUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
