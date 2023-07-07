namespace CoupleCoinApi.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        bool VerifyIfUserIsActiveByUsername(string username);
        bool VerifyPassword(string password, string username);
        bool ChangePassword(string newPassword, string username);
        bool ChangeEmail(string newEmail, string username);
        bool VerifyEmail(string newEmail);
    }
}
