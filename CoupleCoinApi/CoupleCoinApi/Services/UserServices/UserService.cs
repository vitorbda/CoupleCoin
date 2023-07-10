using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
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

        public bool ChangeEmail(string newEmail, string username)
        {
            var user = _userRepository.GetActiveUserByUserName(username);

            user.Email = newEmail;
            user.EmailConfirmed = false;

            var userChanged = _userRepository.UpdateUser(user);
            return userChanged;
        }
        #endregion

        public bool ChangePassword(string newPassword, string username)
        {
            var password = EncryptService.ConvertToSHA256Hash(newPassword);
            var user = _userRepository.GetActiveUserByUserName(username);

            user.Password = password;

            var passwordChanged = _userRepository.UpdateUser(user);

            return passwordChanged;
        }

        public async Task<bool> VerifyEmail(string newEmail)
        {
            var verifyEmail = _userRepository.GetUserByEmail(newEmail);
            if (verifyEmail.UserName != null)
                return false;

            return true;
        }

        public async Task<bool> VerifyIfUserIsActiveByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            var userIsActive = _userRepository.GetActiveUserByUserName(username);
            if (userIsActive == null || string.IsNullOrEmpty(userIsActive.UserName)) 
                return false;

            return true;
        }

        public async Task<bool> VerifyPassword(string password, string username)
        {            
            var userToVerify = _userRepository.GetActiveUserByUserName(username);
            if (userToVerify == null || string.IsNullOrEmpty(userToVerify.UserName))
                return false;

            password = EncryptService.ConvertToSHA256Hash(password);
            if (userToVerify.Password == password)
                return true;

            return false;
        }

        private User ConvertUserDTOToUser(UserDTO userDT, string username)
        {
            try
            {
                var userToReturn = new User
                {
                    UserName = username,
                    Name = userDT.Name,
                    LastName = userDT.LastName,
                    CPF = userDT.CPF,
                    Email = userDT.Email                    
                };

                return userToReturn;
            }
            catch {
                return new User();
            }
        }
    }
}
