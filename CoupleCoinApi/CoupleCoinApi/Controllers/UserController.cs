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
        public IActionResult ChangePassword([FromBody]userPassword userPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = User.Identity.Name;
            var verifyPassword = _userService.VerifyPassword(userPassword.ConfirmPassword, user);
            if (!verifyPassword)
                return BadRequest("Senhas não conferem!");

            var passwordIsValid = _registerService.ValidatePassword(userPassword.NewPassword);
            if (!passwordIsValid.Valid)
                return BadRequest(passwordIsValid.Message);

            var passwordChanged = _userService.ChangePassword(userPassword.NewPassword, user);
            if (!passwordChanged)
                return StatusCode(500);

            return Ok("Senha alterada com sucesso!");
        }

        public class userPassword
        {
            [Required]
            public string ConfirmPassword { get; set; }
            [Required]
            public string NewPassword { get; set; }
        }
    }
}
