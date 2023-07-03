using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.CoupleServices.Interfaces;

namespace CoupleCoinApi.Services.CoupleServices
{
    public class CoupleService : ICoupleService
    {
        #region Constructor
        private readonly IUserRepository _userRepository;
        private readonly ICoupleRepository _coupleRepository;
        public CoupleService(IUserRepository userRepository, 
                                ICoupleRepository coupleRepository) 
        { 
            _userRepository = userRepository;
            _coupleRepository = coupleRepository;
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

        public ValidateRegisterModel ValidateUserToCouple(string userName)
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

        public ValidateRegisterModel VerifiyExistentCouple(string userName1, string userName2)
        {
            var valid = new ValidateRegisterModel { Valid = false };
            var couple = _coupleRepository.GetActiveCoupleByTwoUserName(userName1, userName2);

            if (couple == null || string.IsNullOrEmpty(couple.Id.ToString()))
            {
                valid.Valid = true;
                return valid;
            }

            valid.Message = "Usuários já vinculados!";
            return valid;
        }
    }
}
