using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUserName(string userName);
    }
}
