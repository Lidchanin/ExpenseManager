using ExpenseManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExpenseManager.Models;
using Xamarin.Forms;

namespace ExpenseManager.ViewModels
{
    public class AddExpenseViewModel : BaseViewModel
    {
        public string ExpenseName { get; set; }
        public double ExpenseCost { get; set; }
        public DateTime ExpenseTimestamp { get; set; }
        public ExpenseCategories Category { get; set; }

        public IEnumerable<string> CategoriesList { get; } = Enum.GetNames(typeof(ExpenseCategories)).ToList();

        public ICommand AddExpenseCommand { get; set; }

        public AddExpenseViewModel()
        {
            AddExpenseCommand = new Command(AddExpense);
        }

        private async void AddExpense()
        {
            await SupportPopupService.ShowLoadingAsync();

            var expense = new Expense
            {
                Name = ExpenseName,
                Cost = ExpenseCost,
                Timestamp = ExpenseTimestamp,
                Category = (int) Category
            };

            var temp = await BackendlessApi.AddExpense(expense);
            expense = temp.Content;

            await SupportPopupService.HideLastPopupAsync();
        }
    }
}