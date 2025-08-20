using ExpenseTracker.Domain.Entities;
using ExpenseTracker.DTOs.Request;

namespace ExpenseTracker.Interface
{
    public interface IExpenseService
    {
        Task<object> GetAllRecords(GetExpenseRequest request);

        Task<Expense> AddRecord(Expense record);

        Task<bool> DeleteRecord(int id);
    }
}
