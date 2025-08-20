namespace ExpenseTracker.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}
