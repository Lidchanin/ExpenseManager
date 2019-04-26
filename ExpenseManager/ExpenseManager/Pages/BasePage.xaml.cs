using ExpenseManager.Services;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage
    {
        protected readonly IServiceClient ApiService = ServiceClient.Instance;
        protected readonly ISupportPopupService SupportPopupService = new SupportPopupService();

        public BasePage()
        {
            InitializeComponent();
        }
    }
}