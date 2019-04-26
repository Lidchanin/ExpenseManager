using System;
using ExpenseManager.Pages.Popups;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage
    {
        public ExpensesPage()
        {
            InitializeComponent();
        }

        private async void AddExpenseButton_OnClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new AddExpensePopup());
        }
    }
}