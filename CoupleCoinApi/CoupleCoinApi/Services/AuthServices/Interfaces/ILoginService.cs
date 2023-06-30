using CoupleCoinApi.Models;
using CoupleCoinApi.Models.ViewModel;

namespace CoupleCoinApi.Services.AuthServices.Interfaces
{
    public interface ILoginService
    {
        UserViewModel Login(LoginModel loginModel);
        User ValidateUser(LoginModel loginModel);
    }
}
