using ExpenseManager.Pages.Popups;
using Rg.Plugins.Popup.Services;
using System;
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

        private async void AddExpenseButton_OnClicked(object sender, EventArgs e) =>
            await PopupNavigation.Instance.PushAsync(new AddExpensePopup());

        private void InitDataButton_Clicked(object sender, EventArgs e) => 
            ViewModel.InitData();

        private void InitDataByMonthButton_Clicked(object sender, EventArgs e) =>
            ViewModel.InitDataByMonth();
    }
}