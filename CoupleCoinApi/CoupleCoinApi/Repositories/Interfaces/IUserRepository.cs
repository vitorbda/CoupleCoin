using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
        bool CreateUser(User user);

        User GetUserByEmail(string email);
    }
}
