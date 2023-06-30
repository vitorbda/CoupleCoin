using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        #region Constructor
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly IUserRepository _userRepository;

        public AuthController(ILoginService loginService, 
                                IRegisterService registerService,
                                IUserRepository userRepository)
        {
            _loginService = loginService;
            _registerService = registerService;
            _userRepository = userRepository;
        }
        #endregion

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginModel login)
        {
            var user = _loginService.Login(login);

            if (string.IsNullOrEmpty(user.UserName))
                return Unauthorized(new { message = "Usuário ou senha inválidos!" });

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validateUser = _registerService.ValidateUser(user);
            if (validateUser.Valid == false)
                return BadRequest(validateUser.Message);

            var successfullyRegistered = _registerService.RegisterUser(user);
            if (!successfullyRegistered)
                return StatusCode(500);

            return Created("/", "Usuário criado com sucesso!");
        }
    }
}
