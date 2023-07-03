using CoupleCoinApi.Models;

namespace CoupleCoinApi.Repositories.Interfaces
{
    public interface ICoupleRepository
    {
        Couple GetActiveCoupleByUser(User user);
        Couple GetActiveCoupleByUsername(string userName);
        Couple GetActiveCoupleByTwoUserName(string userName1, string userName2);
        bool CreateCouple(User user, User user2);
    }
}
