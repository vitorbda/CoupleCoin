using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;

namespace CoupleCoinApi.Services.UserServices
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public bool VerifyIfUserIsActiveByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var userIsActive = _userRepository.GetActiveUserByUserName(username);
            if (userIsActive == null || string.IsNullOrEmpty(userIsActive.Id.ToString())) 
                return false;

            return true;
        }
    }
}
