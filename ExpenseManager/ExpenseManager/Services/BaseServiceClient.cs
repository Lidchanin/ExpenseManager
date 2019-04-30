using ExpenseManager.Helpers;
using ExpenseManager.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public abstract class BaseServiceClient
    {
        private readonly HttpClient _client;

        private const string ContentType = "application/json";

        private static readonly JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        private static readonly JsonSerializerSettings DeserializationSettings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };

        protected BaseServiceClient(HttpClient client) => _client = client;

        protected async Task<CommonResponse<T>> GetAsync<T>(string url) =>
            await ExecuteWithGeneralExceptionHandling(async () =>
            {
                var response = await _client.GetAsync(url);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json, DeserializationSettings);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<BackendlessError>(json, DeserializationSettings);
                    throw new HttpListenerException(error.Code, error.Message);
                }
                else
                {
                    throw new HttpListenerException((int) response.StatusCode, response.ReasonPhrase);
                }
            });

        protected async Task<CommonResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest request) =>
            await ExecuteWithGeneralExceptionHandling(async () =>
            {
                var data = JsonConvert.SerializeObject(request, SerializationSettings);
                var postModel = new StringContent(data, Encoding.UTF8, ContentType);
                var response = await _client.PostAsync(url, postModel);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(json, DeserializationSettings);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<BackendlessError>(json, DeserializationSettings);
                    throw new HttpListenerException(error.Code, error.Message);
                }
                else
                {
                    throw new HttpListenerException((int) response.StatusCode, response.ReasonPhrase);
                }
            });

        protected async Task<CommonResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest request) =>
            await ExecuteWithGeneralExceptionHandling(async () =>
            {
                var data = JsonConvert.SerializeObject(request, SerializationSettings);
                var postModel = new StringContent(data, Encoding.UTF8, ContentType);
                var response = await _client.PutAsync(url, postModel);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(json, DeserializationSettings);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<BackendlessError>(json, DeserializationSettings);
                    throw new HttpListenerException(error.Code, error.Message);
                }
                else
                {
                    throw new HttpListenerException((int) response.StatusCode, response.ReasonPhrase);
                }
            });

        protected async Task<CommonResponse<TResponse>> DeleteAsync<TResponse>(string url) =>
            await ExecuteWithGeneralExceptionHandling(async () =>
            {
                var response = await _client.DeleteAsync(url);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(json, DeserializationSettings);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<BackendlessError>(json, DeserializationSettings);
                    throw new HttpListenerException(error.Code, error.Message);
                }
                else
                {
                    throw new HttpListenerException((int) response.StatusCode, response.ReasonPhrase);
                }
            });

        private static async Task<CommonResponse<T>> ExecuteWithGeneralExceptionHandling<T>(Func<Task<T>> func)
        {
            var response = new CommonResponse<T>();

            try
            {
                if (ConnectivityHelper.GetConnectionStatus())
                {
                    response.IsSuccess = true;
                    response.Content = await func();
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ConnectivityHelper.ConnectionErrorMessage;
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.RequestCanceled)
                {
                    response.IsSuccess = false;
                    response.Message = ConnectivityHelper.TimeOutErrorMessage;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ConnectivityHelper.ConnectionErrorMessage;
                }
            }
            catch (HttpListenerException ex)
            {
                response.IsSuccess = false;
                response.Code = ex.ErrorCode;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}