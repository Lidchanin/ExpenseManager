using System;
using ExpenseManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            MasterPage.listView.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MasterPageItem item))
                return;

            Detail = new NavigationPage((Page) Activator.CreateInstance(item.TargetType));
            MasterPage.listView.SelectedItem = null;
            IsPresented = false;
        }
    }
}