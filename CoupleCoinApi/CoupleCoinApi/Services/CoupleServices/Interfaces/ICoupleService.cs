using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.CoupleServices.Interfaces
{
    public interface ICoupleService
    {
        ValidateRegisterModel ValidateUserToCouple(User user);
        bool CreateCouple (User user, User user2);
    }
}
