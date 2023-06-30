using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.AuthServices.Interfaces
{
    public interface ILoginService
    {
        UserViewModel Login(LoginModel loginModel);
        User ValidateUser(LoginModel loginModel);
    }
}
