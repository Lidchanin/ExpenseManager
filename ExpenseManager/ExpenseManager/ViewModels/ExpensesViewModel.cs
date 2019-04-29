using System.Collections.Generic;
using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class ExpensesViewModel : BaseViewModel
    {
        public IList<Expense> Expenses { get; set; }

        public async void InitData()
        {
            await SupportPopupService.ShowLoadingAsync("Getting Expenses");

            Expenses = (await BackendlessApi.GetExpensesForAllTime()).Content;

            await SupportPopupService.HideLastPopupAsync();
        }
    }
}