using ExpenseManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public interface IServiceClient
    {
        #region Categories operations

        Task<CommonResponse<IList<Category>>> GetCategories();

        Task<CommonResponse<Category>> AddCategory(Category category);

        Task<CommonResponse<Category>> UpdateCategory(Category category);

        Task<CommonResponse<long>> DeleteCategory(Category category);

        #endregion Categories operations

        #region Expenses operations

        Task<CommonResponse<IList<Expense>>> GetExpensesForAllTime();

        Task<CommonResponse<Expense>> AddExpense(Expense expense);

        Task<CommonResponse<Expense>> UpdateExpense(Expense expense);

        Task<CommonResponse<long>> DeleteExpense(Expense expense);

        #endregion Expenses operations
    }
}