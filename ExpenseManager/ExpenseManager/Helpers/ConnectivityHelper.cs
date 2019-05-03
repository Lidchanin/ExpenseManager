using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace ExpenseManager.Helpers
{
    public static class ConnectivityHelper
    {
        public const string ConnectionErrorMessage = "There is no access to the Internet. Check the connection";
        public const string TimeOutErrorMessage = "Timeout Expired.";

        private const string IsConnected = "IsConnected";

        public static void InitConnectionStatus()
        {
            if (!CrossConnectivity.IsSupported)
            {
                SetConnectionStatus(true);
            }

            SetConnectionStatus(CrossConnectivity.Current.IsConnected);
            CrossConnectivity.Current.ConnectivityChanged += CurrentOnConnectivityChanged;
        }

        public static bool GetConnectionStatus() =>
            !CrossConnectivity.IsSupported || CrossConnectivity.Current.IsConnected;

        #region Private methods

        private static void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e) =>
            SetConnectionStatus(e.IsConnected);

        private static void SetConnectionStatus(bool connectivityStatus)
        {
            if (!connectivityStatus)
            {
                if (Application.Current.Properties.ContainsKey(IsConnected))
                {
                    Application.Current.Properties.Remove(IsConnected);
                }
            }
            else
            {
                if (!Application.Current.Properties.ContainsKey(IsConnected))
                {
                    Application.Current.Properties.Add(IsConnected, true);
                }
            }
        }

        #endregion Private methods
    }
}