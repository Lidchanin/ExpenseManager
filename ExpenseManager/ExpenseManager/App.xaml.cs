using ExpenseManager.Helpers;
using ExpenseManager.Pages;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExpenseManager
{
    public partial class App
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "OTk0OTVAMzEzNzJlMzEyZTMwS0dSaDRmQUE0em9BM2ROc0I0TENQN25HS3JibERqMElPNmdZQTh0bGkzUT0=");

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            ConnectivityHelper.InitConnectionStatus();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
