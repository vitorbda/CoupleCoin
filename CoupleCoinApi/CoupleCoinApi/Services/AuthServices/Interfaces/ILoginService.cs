using CoupleCoinApi.Models;
using CoupleCoinApi.Models.ViewModel;

namespace CoupleCoinApi.Services.AuthServices.Interfaces
{
    public interface ILoginService
    {
        Task<UserViewModel> Login(LoginModel loginModel);
        Task<User> ValidateUser(LoginModel loginModel);
    }
}
