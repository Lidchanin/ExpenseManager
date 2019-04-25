using ExpenseManager.Helpers;
using ExpenseManager.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExpenseManager.Services.ServiceClient;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExpenseManager
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            BackendlessAPI.Backendless.InitApp(ApplicationId,
                Device.RuntimePlatform == Device.Android ? AndroidApiKey : IOSApiKey);

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
