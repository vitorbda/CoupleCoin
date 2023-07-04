using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Services.ExpenseServices.Interfaces;
using CoupleCoinApi.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/expense")]
    public class ExpenseController : Controller
    {
        #region Constructor
        private readonly IExpenseService _expenseService;
        private readonly IUserService _userService;

        public ExpenseController(IExpenseService expenseService, IUserService userService)
        {
            _expenseService = expenseService;
            _userService = userService;
        }
        #endregion

        [HttpPost]
        [Route("registerExpense")]
        [Authorize]
        public IActionResult RegisterExpense([FromBody]Expense expense)
        {
            return Ok("teste");
        }

        [HttpGet]
        [Route("getExpense")]
        [Authorize]
        public IActionResult GetExpense(int id)
        {            
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("registerExpenseType")]
        [Authorize]
        public IActionResult RegisterExpenseType([FromBody]ExpenseTypeDTO ETD)
        {
            if (string.IsNullOrEmpty(ETD.Name) || ETD == null)
                return BadRequest();

            var verifyCouple = new ValidateRegisterModel { Valid = true };
            if (!string.IsNullOrEmpty(ETD.OwnerTwo))
            {
                // Verifica se o 2° usuário está ativo e/ou existe
                bool verifyUser = _userService.VerifyIfUserIsActiveByUsername(ETD.OwnerTwo);
                if (verifyUser)
                    return NotFound("O segundo usuário não foi encontrado");

                // Verifica se existe o vínculo de usuários
                verifyCouple = _expenseService.VerifyCouple(ETD);
                if (!verifyCouple.Valid)
                    return BadRequest(verifyCouple.Message);
            }

            var expenseTypeCreated = _expenseService.RegisterExpenseType(ETD);
            if (!expenseTypeCreated)
                return StatusCode(500);

            return Created("/", "");
        }
    }
}
