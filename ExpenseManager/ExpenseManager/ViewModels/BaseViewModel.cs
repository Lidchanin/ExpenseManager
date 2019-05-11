using ExpenseManager.Services;
using System.ComponentModel;

namespace ExpenseManager.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly IBackendlessServiceClient BackendlessApi = BackendlessServiceClient.Instance;
        protected readonly IAzureServiceClient AzureApi = AzureServiceClient.Instance;

        protected readonly ISupportPopupService SupportPopupService = new SupportPopupService();
    }
}