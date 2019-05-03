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

            SkiaCalendarView.SelectedDate = DateTime.Today.AddMonths(1);
            SkiaCalendarView.CurrentDate = DateTime.Today.AddMonths(1);
        }
    }
}