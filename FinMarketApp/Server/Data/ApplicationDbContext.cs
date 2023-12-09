using Duende.IdentityServer.EntityFramework.Options;
using FinMarketApp.Server.Models;
using FinMarketApp.Shared;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinMarketApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        // Add a DbSet for each entity type that you want to include in your model
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<BudgetGoal> BudgetGoals => Set<BudgetGoal>();

        public DbSet<Category> Category => Set<Category>();
    }
}
