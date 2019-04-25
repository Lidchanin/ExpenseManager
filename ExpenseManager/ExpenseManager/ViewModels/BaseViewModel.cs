using ExpenseManager.Services;

namespace ExpenseManager.ViewModels
{
    public class BaseViewModel
    {
        protected readonly IServiceClient ApiService = ServiceClient.Instance;

        public BaseViewModel()
        {

        }
    }
}