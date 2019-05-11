using ExpenseManager.Enums;
using ExpenseManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace ExpenseManager.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public int FirstDayOfWeek { get; set; } = 1;

        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();

        //public CalendarEventCollection CalendarEventCollection { get; set; } = new CalendarEventCollection();

        public StartViewModel()
        {
            TempMethod();
        }

        async void TempMethod()
        {
            var expense = new Expense
            {
                Name = "exp8",
                Cost = 3333,
                Category = ExpenseCategories.Alcohol,
                Timestamp = DateTime.Today
            };

            await AzureApi.AddExpense(expense);

            var list = await AzureApi.GetExpensesForAllTime();
        }

        private void LoadTestData()
        {
            Expenses.Add(new Expense
            {
                Id = "111111",
                Name = "exp1",
                Cost = 123,
                Category = ExpenseCategories.Alcohol,
                Timestamp = new DateTime(2019, 04, 25)
            });
            Expenses.Add(new Expense
            {
                Id = "1111111",
                Name = "exp2",
                Cost = 123123,
                Category = ExpenseCategories.Entertainment,
                Timestamp = new DateTime(2019, 04, 25)
            });
            Expenses.Add(new Expense
            {
                Id = "11111111",
                Name = "exp3",
                Cost = 123123213,
                Category = ExpenseCategories.Meals,
                Timestamp = new DateTime(2019, 04, 25)
            });
            Expenses.Add(new Expense
            {
                Id = "111111111",
                Name = "exp4",
                Cost = 123321,
                Category = ExpenseCategories.Alcohol,
                Timestamp = new DateTime(2019, 04, 26)
            });

            Expenses.Add(new Expense
            {
                Id = "2222",
                Name = "exp5",
                Cost = 2222,
                Category = ExpenseCategories.Alcohol,
                Timestamp = new DateTime(2019, 05, 2)
            });
            Expenses.Add(new Expense
            {
                Id = "22222",
                Name = "exp6",
                Cost = 221322,
                Category = ExpenseCategories.Alcohol,
                Timestamp = new DateTime(2019, 05, 2)
            });
            Expenses.Add(new Expense
            {
                Id = "2222222",
                Name = "exp7",
                Cost = 2222321,
                Category = ExpenseCategories.Meals,
                Timestamp = new DateTime(2019, 05, 2)
            });

            Expenses.Add(new Expense
            {
                Id = "33333",
                Name = "exp8",
                Cost = 3333,
                Category = ExpenseCategories.Alcohol,
                Timestamp = DateTime.Today
            });
        }
    }
}