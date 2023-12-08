namespace FinMarketApp.Server.Models
{
    public class Expenses
    {
        public int ExpenseId { get; set; }
        public int GoalId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public double Amount { get; set;}
        public string Date { get; set;}
     
    }
}
