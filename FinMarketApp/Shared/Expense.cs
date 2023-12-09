namespace FinMarketApp.Shared
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int GoalId { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public double Amount { get; set;} = 0.0;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
