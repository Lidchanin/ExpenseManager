using ExpenseManager.Services;
using System.ComponentModel;

namespace ExpenseManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly IServiceClient ApiService = ServiceClient.Instance;
        protected readonly ISupportPopupService SupportPopupService = new SupportPopupService();
    }
}