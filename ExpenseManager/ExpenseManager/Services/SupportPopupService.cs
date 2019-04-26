using ExpenseManager.Pages.Popups.SupportPopups;
using System.Linq;
using System.Threading.Tasks;
using static Rg.Plugins.Popup.Services.PopupNavigation;

namespace ExpenseManager.Services
{
    public sealed class SupportPopupService : ISupportPopupService
    {
        public async Task ShowLoadingAsync(string loadingText = null)
        {
            await Instance.PushAsync(new LoadingPopup(loadingText));
        }

        public async Task HideLastPopupAsync()
        {
            if (Instance.PopupStack.Any())
                await Instance.PopAsync();
        }
    }
}