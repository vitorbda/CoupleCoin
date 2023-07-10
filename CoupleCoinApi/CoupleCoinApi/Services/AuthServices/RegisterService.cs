using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices.Interfaces;
using System.Text.RegularExpressions;
using XAct.Messages;

namespace CoupleCoinApi.Services.AuthServices
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

        public bool RegisterUser(RegisterModel user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName))
                return false;

            var userToReturn = SeedNewUser(user);

            return _userRepository.CreateUser(userToReturn);
        }

        public async Task<ValidateRegisterModel> ValidatePassword(string password)
        {
            var valid = new ValidateRegisterModel { Valid = false };

            if (string.IsNullOrEmpty(password))
            {
                valid.Message = "Senha vazia!";
                return valid;
            }

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

        private User SeedNewUser(RegisterModel user)
        {
            var userToReturn = new User
            {
                IsActive = true,
                UserName = user.UserName,
                AddDate = DateTime.Now,
                Email = user.Email,
                LastName = user.LastName,
                Name = user.Name,
                Password = EncryptService.ConvertToSHA256Hash(user.Password),
                Role = ""
            };

            return userToReturn;
        }

        public async Task<ValidateRegisterModel> ValidateUser(RegisterModel user)
        {
            var validatePassword = await ValidatePassword(user.Password);
            if (!validatePassword.Valid)
                return new ValidateRegisterModel { Valid = false, Message = validatePassword.Message };

            var validateUsername = _userRepository.GetUserByUserName(user.UserName);
            if (validateUsername != null && !string.IsNullOrEmpty(validateUsername.UserName))
                return new ValidateRegisterModel { Valid = false, Message = "Nome de usuário em uso!" };

            var validateEmail = _userRepository.GetUserByEmail(user.Email);
            if (validateEmail != null && !string.IsNullOrEmpty(validateEmail.Email))
                return new ValidateRegisterModel { Valid = false, Message = "Email já utilizado!" };

            return new ValidateRegisterModel { Valid = true };
        }
    }
}
