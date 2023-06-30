using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.Interfaces;
using System.Text.RegularExpressions;
using XAct.Messages;

namespace CoupleCoinApi.Services
{
    public class RegisterService : IRegisterService
    {
        #region Dependency Injection
        private readonly IUserRepository _userRepository;
        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public bool RegisterUser(User user)
        {
            user = SeedNewUser(user);

            return _userRepository.CreateUser(user);
        }

        public ValidateRegisterModel ValidatePassword(string password)
        {
            var valid = new ValidateRegisterModel { Valid = false };

            if (password.Length < 8)
            {
                valid.Message = "Senha fraca! Mínimo de 8 caracteres";
                return valid;
            }

            if (!password.Any(char.IsUpper))
            {
                valid.Message = "Senha fraca! Necessário ao menos uma letra maiúscula";
                return valid;
            }

            if (!password.Any(char.IsLower))
            {
                valid.Message = "Senha fraca! Necessário ao menos uma letra minúscula";
                return valid;
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*]"))
            {
                valid.Message = "Senha fraca! Necessário ao menos um símbolo";
                return valid;
            }

            if (!password.Any(char.IsDigit))
            {
                valid.Message = "Senha fraca! Necessário ao menos um número";
                return valid;
            }

            valid.Message = "Senha válida!";
            valid.Valid = true;
            return valid;
        }

        private User SeedNewUser(User user)
        {
            user.Id = 0;
            user.IsActive = true;
            user.CreateDate = DateTime.Now;
            user.Role = "Test";
            user.Password = EncryptService.ConvertToSHA256Hash(user.Password);

            return user;
        }            

        public ValidateRegisterModel ValidateUser(User user)
        {
            var validatePassword = ValidatePassword(user.Password);
            if (!validatePassword.Valid)
                return new ValidateRegisterModel { Valid = false, Message = validatePassword.Message };

            var validateUsername = _userRepository.GetUserByUserName(user.UserName);
            if (validateUsername != null)
                return new ValidateRegisterModel { Valid = false, Message = "Nome de usuário em uso!" };

            var validateEmail = _userRepository.GetUserByEmail(user.Email);
            if (validateEmail != null)
                return new ValidateRegisterModel { Valid = false, Message = "Email já utilizado!" };

            return new ValidateRegisterModel { Valid = true };
        }
    }
}
