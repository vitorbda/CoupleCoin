using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services.Interfaces
{
    public interface ILoginService
    {
        string Login(LoginModel loginModel);
        User ValidateUser(LoginModel loginModel);
    }
}
