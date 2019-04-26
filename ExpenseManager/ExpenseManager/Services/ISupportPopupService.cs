using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public interface ISupportPopupService
    {
        Task ShowLoadingAsync(string loadingText = null);

        Task HideLastPopupAsync();
    }
}