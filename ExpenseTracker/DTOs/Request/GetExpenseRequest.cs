namespace ExpenseTracker.DTOs.Request
{
    public class GetExpenseRequest
    {
        public List<string>? Types { get; set; } = [];
        public List<string>? Categories { get; set; } = [];
        public string? Notes { get; set; }
        public List<string>? Months { get; set; } = [];
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageSize { get; set; } = 0;
        public int PageNo { get; set; } = 0;
    }
}
