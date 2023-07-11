using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
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
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseService expenseService, IUserService userService, IExpenseRepository expenseRepository)
        {
            _expenseService = expenseService;
            _userService = userService;
            _expenseRepository = expenseRepository;
        }
        #endregion

        [HttpPost]
        [Route("registerExpense")]
        [Authorize]
        public async Task<IActionResult> RegisterExpense([FromBody]ExpenseDTO expense)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            expense.UsernameOne = User.Identity.Name;

            if (!string.IsNullOrEmpty(expense.UsernameTwo))
            {
                var verifyCouple = await _expenseService.VerifyCouple(expense.UsernameOne, expense.UsernameTwo);
                if (!verifyCouple.Valid)
                    return BadRequest(verifyCouple.Message);
            }

            var verifyExpenseType = _expenseService.VerifyExpenseType(expense.ExpenseTypeId, expense.UsernameOne,expense.UsernameTwo);
            if (!verifyExpenseType.Valid)
            {
                var statusCode = verifyExpenseType.StatusCode;

                if (statusCode == 404)
                    return NotFound(verifyExpenseType.Message);

                if (statusCode == 401)
                    return Unauthorized(verifyExpenseType.Message);
            }

            var expenseRegistered = _expenseService.RegisterExpense(expense);
            if (!expenseRegistered)
                return StatusCode(500);

            return Ok("Cadastrado com sucesso!");
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
        public async Task<IActionResult> RegisterExpenseType([FromBody]ExpenseTypeDTO ETD)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(ETD.OwnerTwo))
            {
                var verifyCouple = await _expenseService.VerifyCouple(ETD.Owner, ETD.OwnerTwo);
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
