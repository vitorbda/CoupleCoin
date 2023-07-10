using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.AuthServices.Interfaces
{
    public interface IRegisterService
    {
        bool RegisterUser(RegisterModel user);
        Task<ValidateRegisterModel> ValidatePassword(string password);
        Task<ValidateRegisterModel> ValidateUser(RegisterModel user);
    }
}
