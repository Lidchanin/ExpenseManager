using System;
using System.Windows.Input;
using ExpenseManager.Models;
using Xamarin.Forms;

namespace ExpenseManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string ExpenseName { get; set; }
        public double ExpenseCost { get; set; }
        public DateTime ExpenseTimestamp { get; set; } = DateTime.Now;
        public string CategoryName{ get; set; }

        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            AddCommand = new Command(async () =>
            {
                var categoryResponse = await ApiService.AddCategory(new Category {Name = CategoryName});
                if (categoryResponse.IsSuccess)
                {
                    var expenseResponse = await ApiService.AddExpense(new Expense
                    {
                        Category = categoryResponse.Content,
                        Cost = ExpenseCost,
                        Name = ExpenseName,
                        Timestamp = ExpenseTimestamp
                    });
                }
            });
        }
    }
}