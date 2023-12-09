using FinMarketApp.Server.Data;
using FinMarketApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using System.Diagnostics;

namespace FinMarketApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // constructor
        public ExpenseController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // ================================ Get Expenses for this user base on goal id and user id ================================
        [HttpGet("{goalId}")]
        public async Task<ActionResult<List<Expense>>> GetExpensesByUserAndGoal(int goalId)  
        {
            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)); // authenticated user object from the database

            if (user == null)
            {
                return NotFound();
            }

            var budgetGoal = await _context.BudgetGoals.FirstOrDefaultAsync(b => b.BudgetGoalId == goalId);

            if (budgetGoal == null)
            {
                return NotFound("Sorry no single budget found.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId


            var expenses = await _context.Expenses.Where(e => e.GoalId == goalId && e.ApplicationUserId == userId).ToListAsync();

            if (expenses == null)
            {
                return NotFound("Sorry no expenses found.");
            }

            return Ok(expenses);
        }

        // ================================ Add Expense ================================
        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(Expense expense)
        {
            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)); // authenticated user object from the database

            if (user == null)
            {
                return NotFound();
            }

            var budgetGoal = await _context.BudgetGoals.FirstOrDefaultAsync(b => b.BudgetGoalId == expense.GoalId);

            if (budgetGoal == null)
            {
                return NotFound("Sorry no single budget found.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            expense.ApplicationUserId = userId;

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return Ok(expense);
        }

        // ================================ Delete Expense ================================
        [HttpDelete("{expenseId}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int expenseId)
        {
            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)); // authenticated user object from the database

            if (user == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.ExpenseId == expenseId);

            if (expense == null)
            {
                return NotFound("Sorry no single expense found.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            if (expense.ApplicationUserId != userId)
            {
                return BadRequest("Sorry you are not authorized to delete this expense.");
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return Ok(expense);
        }   


    }
}
