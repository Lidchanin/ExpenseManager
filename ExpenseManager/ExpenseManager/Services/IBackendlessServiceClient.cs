using ExpenseManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public interface IBackendlessServiceClient
    {
        /// <summary>
        /// Get all <see cref="Expense"/>s for all time (async).
        /// </summary>
        /// <returns><see cref="List{T}"/> of <see cref="Expense"/></returns>
        Task<CommonResponse<List<Expense>>> GetExpensesForAllTime();

        /// <summary>
        /// Get <see cref="Expense"/>s for entered month and year (async).
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <param name="sortedByAsc">If <paramref name="sortedByAsc"/> == true,
        ///                           response collection sorted by asc,
        ///                           else - by desc.</param>
        /// <returns><see cref="List{T}"/> of <see cref="Expense"/></returns> 
        Task<CommonResponse<List<Expense>>> GetExpensesForMonthAndYear(int month, int year, bool sortedByAsc = false);

        /// <summary>
        /// Add <see cref="Expense"/> (async).
        /// </summary>
        /// <param name="expense"><see cref="Expense"/> to add.</param>
        /// <returns>Current <see cref="Expense"/> with server Id</returns>
        Task<CommonResponse<Expense>> AddExpense(Expense expense);

        /// <summary>
        /// Update <see cref="Expense"/> (async).
        /// </summary>
        /// <param name="expense"><see cref="Expense"/> to update.</param>
        /// <returns>Updated <see cref="Expense"/></returns>
        Task<CommonResponse<Expense>> UpdateExpense(Expense expense);

        /// <summary>
        /// Delete <see cref="Expense"/> (async).
        /// </summary>
        /// <param name="expenseId">Deletion <see cref="Expense"/>.Id</param>
        /// <returns>DeletionTime in milliseconds</returns>
        Task<CommonResponse<long>> DeleteExpense(string expenseId);
    }
}