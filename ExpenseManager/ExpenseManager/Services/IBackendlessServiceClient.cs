using ExpenseManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public interface IBackendlessServiceClient
    {
        #region Expenses operations

        Task<CommonResponse<IList<Expense>>> GetExpensesForAllTime();

        Task<CommonResponse<Expense>> AddExpense(Expense expense);

        Task<CommonResponse<Expense>> UpdateExpense(Expense expense);

        Task<CommonResponse<long>> DeleteExpense(Expense expense);

        #endregion Expenses operations
    }
}