using ExpenseManager.Helpers;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages.Popups.SupportPopups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopup
    {
        public LoadingPopup(string loadingText = null)
        {
            InitializeComponent();

            LoadingLabel.Text = string.IsNullOrEmpty(loadingText) ? ConstantHelper.DefaultLoadingText : loadingText;
        }

        protected override bool OnBackButtonPressed() => true;

        protected override bool OnBackgroundClicked() => false;
    }
}