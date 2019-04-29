using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddExpensePopup
	{
		public AddExpensePopup ()
		{
			InitializeComponent ();
		}

	    protected override bool OnBackButtonPressed() => true;

	    protected override bool OnBackgroundClicked() => false;

	    private async void CancelButton_OnClicked(object sender, EventArgs e) =>
	        await PopupNavigation.Instance.PopAsync();
	}
}