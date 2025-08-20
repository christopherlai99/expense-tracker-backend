using ExpenseTracker.Domain.Entities;
using ExpenseTracker.DTOs.Request;
using ExpenseTracker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/expense-tracker")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseService _expenseService;

        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpPost("get-expense")]
        public async Task<IActionResult> GetExpense([FromBody] GetExpenseRequest request)
        {
            try
            {
                var allExpenses = await _expenseService.GetAllRecords(request);
                return Ok(allExpenses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("add-expense")]
        public async Task<IActionResult> AddExpense([FromBody] Expense record)
        {
            try
            {
                var addedExpense = await _expenseService.AddRecord(record);
                return Ok(new { success = addedExpense, message = "Added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("update-expense")]
        public async Task<IActionResult> UpdateExpense([FromBody] Expense record)
        {
            try
            {
                if(record.Id == 0)
                {
                    throw new Exception("Invalid record.");
                }
                var updateExpense = await _expenseService.UpdateRecord(record);
                return Ok(new { success = updateExpense, message = "Updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete-expense/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                var delete = await _expenseService.DeleteRecord(id);
                return Ok(new { success = delete, message = "Deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
