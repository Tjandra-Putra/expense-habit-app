using FinMarketApp.Server.Data;
using FinMarketApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using System.Diagnostics;

namespace FinMarketApp.Server.Controllers
{
    [Authorize] // only authenticated users can access this controller
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetGoalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        // constructor
        public BudgetGoalController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // ================================ Get Budget Goals for this user ================================
        [HttpGet]
        public async Task<ActionResult<List<BudgetGoal>>> GetBudgetGoals() // this returns specific list of budgets for this user
        {
            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)); // authenticated user object from the database

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.BudgetGoals);
        }


        // ================================ Get single budget goal ================================
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetGoal>> GetSingleBudgetGoal(int id)
        {
            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)); // authenticated user object from the database

            if (user == null)
            {
                return NotFound("Sorry no user found.");
            }

            var budgetGoal = await _context.BudgetGoals.FirstOrDefaultAsync(b => b.BudgetGoalId == id);

            if (budgetGoal == null)
            {
                return NotFound("Sorry no single budget found.");
            }

            return Ok(budgetGoal);
        }

        // ================================ Create new budget goal ================================
        [HttpPost]
        public async Task<ActionResult<BudgetGoal>> CreateBudgetGoal(BudgetGoal budgetGoal)
        {
            Debug.WriteLine("BUDGET GOAL CONTROLLER ACTIVATED! ");

            var user = await _context.Users
                .Include(u => u.BudgetGoals)
                .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return NotFound("Sorry, no user found.");
            }

            try
            {
                // Set the ApplicationUserId for the budget goal
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                budgetGoal.ApplicationUserId = userId;

                _context.BudgetGoals.Add(budgetGoal);

                await _context.SaveChangesAsync();

                Debug.WriteLine("Budget goal saved successfully!");


                // Retrieve the updated list of budget goals
                var updatedBudgetGoals = await GetBudgetGoals();

                return Ok(await GetBudgetGoals());

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving budget goal: {ex.Message}");
                return BadRequest("Error saving budget goal.");
            }
        }

        // ================================ Delete Budget Goal by Id ================================
        [HttpDelete("{id}")]
        public async Task<ActionResult<BudgetGoal>> DeleteBudgetGoal(int id)
        {
            var budgetGoal = await _context.BudgetGoals.FindAsync(id);

            if (budgetGoal == null)
            {
                return NotFound("Sorry, no budget goal found.");
            }

            _context.BudgetGoals.Remove(budgetGoal);

            await _context.SaveChangesAsync();

            return Ok(budgetGoal);
        }

        // ================================ Update Budget Goal by Id ================================
        [HttpPut("{id}")]
        public async Task<ActionResult<BudgetGoal>> UpdateBudgetGoal(int id, BudgetGoal clientBudgetGoal)
        {
          
            var budgetGoal = await _context.BudgetGoals.FindAsync(id);

            if (budgetGoal == null)
            {
                return NotFound("Sorry, no budget goal found.");
            }

            budgetGoal.Name = clientBudgetGoal.Name;
            budgetGoal.Target = clientBudgetGoal.Target;

            await _context.SaveChangesAsync();

            return Ok(budgetGoal);

        }
        
    }
}
