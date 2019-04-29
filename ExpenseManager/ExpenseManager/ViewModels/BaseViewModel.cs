using ExpenseManager.Services;
using System.ComponentModel;

namespace ExpenseManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly IBackendlessServiceClient BackendlessApi = BackendlessServiceClient.Instance;

        protected readonly ISupportPopupService SupportPopupService = new SupportPopupService();
    }
}