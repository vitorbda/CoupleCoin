using CoupleCoinApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoupleCoinApi.Controllers
{
    [Route("v1/expense")]
    public class ExpenseController : Controller
    {

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
        public IActionResult RegisterExpenseType(int id)
        {
            throw new NotImplementedException();
        }
    }
}
