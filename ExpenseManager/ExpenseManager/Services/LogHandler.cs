using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public class LogHandler : DelegatingHandler
    {
        public LogHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
#if DEBUG
            Debug.WriteLine($"{request.Method}:{request.RequestUri}");
            if (request.Content != null)
                Debug.WriteLine($"request:{await request.Content.ReadAsStringAsync()}");
#endif

            var response = await base.SendAsync(request, cancellationToken);

#if DEBUG
            if (response.Content != null)
                Debug.WriteLine($"response:{await response.Content.ReadAsStringAsync()}");
#endif
            return response;
        }
    }
}