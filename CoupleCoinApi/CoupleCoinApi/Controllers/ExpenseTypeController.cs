using CoupleCoinApi.DTO;
using CoupleCoinApi.Services.CoupleServices.Interfaces;
using CoupleCoinApi.Services.ExpenseTypeServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/expensetype")]
    public class ExpenseTypeController : Controller
    {
        #region Constructor
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly ICoupleService _coupleService;
        public ExpenseTypeController(IExpenseTypeService expenseTypeService, 
                                        ICoupleService coupleService)
        {
            _expenseTypeService = expenseTypeService;
            _coupleService = coupleService;
        }
        #endregion
        
        [HttpPost]
        [Route("post")]
        [Authorize]
        public async Task<IActionResult> PostExpenseType([FromBody] ExpenseTypeDTO ETD)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(ETD.OwnerTwo))
            {
                var verifyCouple = await _coupleService.VerifiyExistentCouple(ETD.Owner, ETD.OwnerTwo);
                if (!verifyCouple.Valid)
                    return BadRequest(verifyCouple.Message);
            }

            var expenseTypeCreated = _expenseTypeService.RegisterExpenseType(ETD);
            if (!expenseTypeCreated)
                return StatusCode(500);

            return Created("/", "");
        }
    }
}
