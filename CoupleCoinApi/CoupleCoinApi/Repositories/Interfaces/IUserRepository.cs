using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
        User GetActiveUserByUserName(string userName);
        bool CreateUser(User user);
        User GetUserByEmail(string email);
        bool UpdateUser (User user);
    }
}
