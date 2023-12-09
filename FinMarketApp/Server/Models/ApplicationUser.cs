using FinMarketApp.Shared;
using Microsoft.AspNetCore.Identity;

namespace FinMarketApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<BudgetGoal> BudgetGoals { get; set; } = new List<BudgetGoal>();


    }
}
