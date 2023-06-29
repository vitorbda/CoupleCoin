using CoupleCoinApi.Models;
using CoupleCoinApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/account")]
    public class AuthController : Controller
    {
        #region Dependency Injection
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
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

            return Ok(new { token = token });
        }
    }
}
