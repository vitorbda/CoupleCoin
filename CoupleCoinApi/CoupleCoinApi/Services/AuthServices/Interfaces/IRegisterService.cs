using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.AuthServices.Interfaces
{
    public interface IRegisterService
    {
        bool RegisterUser(RegisterModel user);
        ValidateRegisterModel ValidatePassword(string password);
        ValidateRegisterModel ValidateUser(RegisterModel user);
    }
}
