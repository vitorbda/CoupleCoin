namespace CoupleCoinApi.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        bool VerifyIfUserIsActiveByUsername(string username);
    }
}
