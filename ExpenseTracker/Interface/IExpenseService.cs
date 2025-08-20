using ExpenseTracker.Domain.Entities;
using ExpenseTracker.DTOs.Request;

namespace ExpenseTracker.Interface
{
    public interface IExpenseService
    {
        Task<object> GetAllRecords(GetExpenseRequest request);

        Task<bool> AddRecord(Expense record);

        Task<bool> UpdateRecord(Expense record);

        Task<bool> DeleteRecord(int id);
    }
}
