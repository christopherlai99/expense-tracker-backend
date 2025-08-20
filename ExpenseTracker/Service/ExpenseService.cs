using ExpenseTracker.Domain.Entities;
using ExpenseTracker.DTOs.Request;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _dbContext;

        public ExpenseService(AppDbContext dbContext) { _dbContext = dbContext; }

        public async Task<object> GetAllRecords(GetExpenseRequest request)
        {
            var baseQuery = _dbContext.Expenses.AsNoTracking().AsQueryable();
            
            if(request.Months != null && request.Months.Count > 0)
            {
                if (!request.Months.Any(m => m.Equals("All", StringComparison.OrdinalIgnoreCase)))
                {
                    var yearMonthNum = request.Months
                    .Select(m => DateTime.ParseExact(m, "yyyy-MM", null))
                    .Select(d => d.Year * 100 + d.Month)
                    .ToList();

                    baseQuery = baseQuery.Where(e => yearMonthNum.Contains(e.ExpenseDate.Year * 100 + e.ExpenseDate.Month));
                }
            }

            if (request.Types != null && request.Types.Count > 0)
            {
                if (!request.Types.Any(m => m.Equals("All", StringComparison.OrdinalIgnoreCase)))
                {
                    baseQuery = baseQuery.Where(e => request.Types.Contains(e.Type));
                }
            }

            if (request.Categories != null && request.Categories.Count > 0)
            {
                if (!request.Categories.Any(m => m.Equals("All", StringComparison.OrdinalIgnoreCase)))
                {
                    baseQuery = baseQuery.Where(e => request.Categories.Contains(e.Category));
                }
            }

            if (!string.IsNullOrEmpty(request.Notes))
            {
                baseQuery = baseQuery.Where(e => e.Note != null && e.Note.Contains(request.Notes));
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                baseQuery = baseQuery.Where(t => t.ExpenseDate >= request.StartDate && t.ExpenseDate <= request.EndDate);
            }
            var totalQuery = baseQuery;
            baseQuery = baseQuery.OrderByDescending(e => e.CreatedDateTime ?? DateTime.MinValue)
                .ThenBy(e => e.Id);
            
            if (request.PageNo != 0 && request.PageSize != 0)
            {
                int skip = (request.PageNo - 1) * request.PageSize;
                baseQuery = baseQuery.Skip(skip).Take(request.PageSize);
            }
            else
            {
                baseQuery = baseQuery.Take(99);
            }
            var totalRecord = await totalQuery.CountAsync();
            var expenseList = await baseQuery.ToListAsync();
            return new { expenses= expenseList, totalRecord  };
        }

        public async Task<bool> AddRecord(Expense record)
        {
            try
            {
                _dbContext.Add(record);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateRecord(Expense record)
        {
            try
            {
                var expense = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == record.Id);
                if (expense == null)
                {
                    throw new InvalidOperationException("Record Not Found.");
                }
                expense.ExpenseDate = record.ExpenseDate;
                expense.Amount = record.Amount;
                expense.Category = record.Category;
                expense.Note = record.Note;
                expense.Type = record.Type;
                _dbContext.Update(expense);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteRecord(int id)
        {
            try
            {
                var record = await _dbContext.Expenses.SingleOrDefaultAsync(x => x.Id == id);
                if (record == null)
                {
                    throw new InvalidOperationException("Record Not Found.");
                }
                _dbContext.Expenses.Remove(record);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
