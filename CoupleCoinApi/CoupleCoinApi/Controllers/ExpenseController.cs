using CoupleCoinApi.DTO;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using CoupleCoinApi.Services.CoupleServices.Interfaces;
using CoupleCoinApi.Services.ExpenseServices.Interfaces;
using CoupleCoinApi.Services.ExpenseTypeServices.Interfaces;
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
        private readonly ICoupleService _coupleService;
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseController(IExpenseService expenseService,
                                    ICoupleService coupleService,
                                    IExpenseTypeService expenseTypeService)
        {
            _expenseService = expenseService;
            _coupleService = coupleService;
            _expenseTypeService = expenseTypeService;
        }
        #endregion

        [HttpPost]
        [Route("post")]
        [Authorize]
        public async Task<IActionResult> PostExpense([FromBody]ExpenseDTO expense)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            expense.UsernameOne = User.Identity.Name;

            if (!string.IsNullOrEmpty(expense.UsernameTwo))
            {
                var verifyCouple = await _coupleService.VerifiyExistentCouple(expense.UsernameOne, expense.UsernameTwo);
                if (!verifyCouple.Valid)
                    return BadRequest(verifyCouple.Message);
            }

            var verifyExpenseType = _expenseTypeService.VerifyExpenseType(expense.ExpenseTypeId, expense.UsernameOne,expense.UsernameTwo);
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
    }
}
