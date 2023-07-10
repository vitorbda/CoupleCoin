using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.CoupleServices.Interfaces
{
    public interface ICoupleService
    {
        Task<ValidateRegisterModel> ValidateUserToCouple(string userName);
        bool CreateCouple (string userName1, string userName2);
        Task<ValidateRegisterModel> VerifiyExistentCouple (string userName1, string userName2);
    }
}
