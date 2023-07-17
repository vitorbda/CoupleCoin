using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.CoupleServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using XAct.Users;

namespace CoupleCoinApi.Services.CoupleServices
{
    public class CoupleService : ICoupleService
    {
        #region Constructor
        private readonly IUserRepository _userRepository;
        private readonly ICoupleRepository _coupleRepository;
        private readonly IUserService _userService;
        public CoupleService(IUserRepository userRepository, 
                                ICoupleRepository coupleRepository, 
                                IUserService userService) 
        { 
            _userRepository = userRepository;
            _coupleRepository = coupleRepository;
            _userService = userService;
        }
        #endregion

        public bool CreateCouple(string userName1, string userName2)
        {
            var user1 = _userRepository.GetActiveUserByUserName(userName1);
            var user2 = _userRepository.GetActiveUserByUserName(userName2);

            var sucessfullyRegistered = _coupleRepository.CreateCouple(user1, user2);

            if (!sucessfullyRegistered)
                return false;

            return true;
        }

        public async Task<ValidateRegisterModel> ValidateUserToCouple(string userName)
        {
            var valid = new ValidateRegisterModel { Valid = false };
            var user = _userRepository.GetActiveUserByUserName(userName);

            if (user == null || string.IsNullOrEmpty(user.UserName))
            {
                valid.StatusCode = 401;
                valid.Message = "Usuário para colaboração inválido!";
                return valid;
            }

            valid.Valid = true;
            return valid;
        }

        public async Task<ValidateRegisterModel> VerifiyExistentCouple(string username, string usernametwo)
        {
            var valid = new ValidateRegisterModel { Valid = false };

            if (string.IsNullOrEmpty(usernametwo) || string.IsNullOrEmpty(username))
            {
                valid.Message = "Necessário dois usuários para o vínculo";
                return valid;
            }

            var verifyUser = await _userService.VerifyIfUserIsActiveByUsername(usernametwo);
            if (!verifyUser)
            {
                valid.Message = "O segundo usuário não foi encontrado";
                return valid;
            }

            var coupleToVerify = _coupleRepository.GetActiveCoupleByTwoUserName(username, usernametwo);
            if (coupleToVerify == null || coupleToVerify.User1 == null)
            {
                valid.Message = "Vínculo de usuários não encontrado";
                return valid;
            }

            valid.Message = "Vínculo de usuários existe!";
            valid.Valid = true;
            return valid;
        }
    }
}
