using ExpenseManager.Services;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage
    {
        public readonly IServiceClient ApiService = ServiceClient.Instance;

        public BasePage()
        {
            InitializeComponent();
        }
    }
}