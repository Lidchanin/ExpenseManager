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
	}
}