using System;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();

        }

        private void MoveToCurrentButton_OnClicked(object sender, EventArgs e)
        {
            CalendarView.MoveToDate = DateTime.Today;
        }
    }
}