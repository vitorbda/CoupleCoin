using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface ICoupleRepository
    {
        Couple GetActiveCoupleByUser(User user);
        Couple GetActiveCoupleByUsername(string username);
        bool CreateCouple(User user, User user2);
    }
}
