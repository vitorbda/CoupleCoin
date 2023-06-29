using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.Interfaces;
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
            var token = _loginService.Login(login);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Usuário ou senha inválidos!" });

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validatePassword = _registerService.ValidatePassword(user.Password);
            if (!validatePassword.Valid)            
                return BadRequest(validatePassword.Message);

            var validateUsername = _userRepository.GetUserByUserName(user.UserName);
            if (validateUsername != null)
                return BadRequest("Nome de usuário em uso!");
            
            var validateEmail = _userRepository.GetUserByEmail(user.Email);
            if (validateEmail != null)
                return BadRequest("Email já utilizado!");

            var successfullyRegistered = _registerService.RegisterUser(user);
            if (!successfullyRegistered)
                return StatusCode(500);

            return Created("/", "Usuário criado com sucesso!");
        }
    }
}
