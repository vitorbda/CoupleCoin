using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.AuthServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
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
        private readonly IUserService _userService;
        public AuthController(ILoginService loginService, 
                                IRegisterService registerService,
                                IUserRepository userRepository,
                                IUserService userService)
        {
            _loginService = loginService;
            _registerService = registerService;
            _userService = userService;
            _userRepository = userRepository;
        }
        #endregion

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginModel login)
        {
            var genericReturn = "Usuário ou senha inválidos!";
            if (!ModelState.IsValid)
                return Unauthorized(genericReturn);

            var userValid = await _loginService.ValidateUser(login);

            var userVM = await _loginService.Login(login);

            if (string.IsNullOrEmpty(userVM.UserName))
                return Unauthorized(genericReturn);

            return Ok(userVM);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var validateUser = await _registerService.ValidateUser(user);

            if (validateUser.Valid == false)
                return BadRequest(validateUser.Message);

            var successfullyRegistered = _registerService.RegisterUser(user);
            if (!successfullyRegistered)
                return StatusCode(500);

            return Created("/", "Usuário criado com sucesso!");
        }
    }
}
