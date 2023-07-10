namespace CoupleCoinApi.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<bool> VerifyIfUserIsActiveByUsername(string username);
        Task<bool> VerifyPassword(string password, string username);
        bool ChangePassword(string newPassword, string username);
        bool ChangeEmail(string newEmail, string username);
        Task<bool> VerifyEmail(string newEmail);
    }
}
