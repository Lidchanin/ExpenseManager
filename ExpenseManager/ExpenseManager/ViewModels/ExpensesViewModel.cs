using ExpenseManager.Models;
using System.Collections.ObjectModel;

namespace ExpenseManager.ViewModels
{
    public class ExpensesViewModel : BaseViewModel
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public int Month { get; set; } = 1;
        public int Year { get; set; } = 2019;

        public async void InitData()
        {
            await SupportPopupService.ShowLoadingAsync("Getting Expenses");

            Expenses = new ObservableCollection<Expense>((await BackendlessApi.GetExpensesForAllTime()).Content);

            await SupportPopupService.HideLastPopupAsync();
        }

        public async void InitDataByMonth()
        {
            await SupportPopupService.ShowLoadingAsync("Getting Expenses by Month & Year");

            Expenses = new ObservableCollection<Expense>((await BackendlessApi.GetExpensesForMonthAndYear(Month, Year))
                .Content);

            await SupportPopupService.HideLastPopupAsync();
        }
    }
}