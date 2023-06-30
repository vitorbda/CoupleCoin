using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.Interfaces
{
    public interface IRegisterService
    {
        bool RegisterUser(User user);
        ValidateRegisterModel ValidatePassword(string password);
        ValidateRegisterModel ValidateUser(User user);
    }
}
