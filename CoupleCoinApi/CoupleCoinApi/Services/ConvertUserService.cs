using CoupleCoinApi.Models;

namespace CoupleCoinApi.Services
{
    public static class ConvertUserService
    {
        public static UserViewModel ConvertUserToUserViewModel(User user)
        {
            return new UserViewModel
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                LastName = user.LastName
            };
        }
    }
}
