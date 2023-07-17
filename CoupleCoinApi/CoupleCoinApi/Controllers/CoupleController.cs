using CoupleCoinApi.Services.CoupleServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/couple")]
    public class CoupleController : Controller
    {
        private readonly ICoupleService _coupleService;
        public CoupleController(ICoupleService coupleService) 
        { 
            _coupleService = coupleService;
        }

        [HttpPost]
        [Route("registerCouple")]
        [Authorize]
        public async Task<IActionResult> RegisterCouple(string userNameToCouple)
        {
            if (string.IsNullOrEmpty(userNameToCouple))
                return BadRequest("Usuário para colaboração não pode ser vazio!");

            var userName = User.Identity.Name;

            var userIsValidTask = _coupleService.ValidateUserToCouple(userNameToCouple);
            var coupleExistsTask = _coupleService.VerifiyExistentCouple(userName, userNameToCouple);

            var userIsValid = await userIsValidTask;
            var coupleExists = await coupleExistsTask;

            if (!userIsValid.Valid)
                return BadRequest(userIsValid.Message);

            if (coupleExists.Valid)
                return BadRequest(coupleExists.Message);

            var coupleWasCreated = _coupleService.CreateCouple(userName, userNameToCouple);
            if (!coupleWasCreated)
                return StatusCode(500);

            return Created("/", "Vínculo cirado com sucesso!");
        }

        
    }
}
