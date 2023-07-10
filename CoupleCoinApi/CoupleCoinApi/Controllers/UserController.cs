using CoupleCoinApi.Services.AuthServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/user")]
    public class UserController : Controller
    {
        #region Constructor
        private readonly IUserService _userService;
        private readonly IRegisterService _registerService;

        public UserController(IUserService userService, IRegisterService registerService)
        {
            _userService = userService;
            _registerService = registerService;
        }
        #endregion

        [HttpPut]
        [Route("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]userPassword userPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = User.Identity.Name;
            var verifyPasswordTask = _userService.VerifyPassword(userPassword.ConfirmPassword, user);
            var passwordIsValidTask = _registerService.ValidatePassword(userPassword.NewPassword);

            var verifyPassword = await verifyPasswordTask;
            var passwordIsValid = await passwordIsValidTask;

            if (!verifyPassword)
                return BadRequest("Senhas não conferem!");
            
            if (!passwordIsValid.Valid)
                return BadRequest(passwordIsValid.Message);

            var passwordChanged = _userService.ChangePassword(userPassword.NewPassword, user);
            if (!passwordChanged)
                return StatusCode(500);

            return Ok("Senha alterada com sucesso!");
        }

        [HttpPut]
        [Route("changeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody]userEmail userEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = User.Identity.Name;
            var verifyPasswordTask = _userService.VerifyPassword(userEmail.Password, user);
            var verifyEmailTask = _userService.VerifyEmail(userEmail.NewEmail);

            var verifyPassword = await verifyPasswordTask;
            var verifyEmail = await verifyEmailTask;

            if (!verifyPassword)
                return BadRequest("Senha incorreta!");

            if (!verifyEmail)
                return BadRequest("Email já está em uso!");

            var emailChanged = _userService.ChangeEmail(userEmail.NewEmail, user);
            if (!emailChanged)
                return StatusCode(500);

            return Ok("Email alterado com sucesso");
        }

        public class userPassword
        {
            [Required]
            public string ConfirmPassword { get; set; }
            [Required]
            public string NewPassword { get; set; }
        }
        public class userEmail
        {
            [Required]
            public string ConfirmEmail { get; set; }
            [Required]
            public string NewEmail { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
