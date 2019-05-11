using System.Collections.Generic;
using ExpenseManager.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public interface IAzureServiceClient
    {
        Task<CommonResponse<List<Expense>>> GetExpensesForAllTime();

        Task<CommonResponse<Expense>> AddExpense(Expense expense);
    }
}