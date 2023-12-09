namespace FinMarketApp.Shared
{
    public class BudgetGoal
    {
        public int BudgetGoalId { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Target { get; set;} = 0.0;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
