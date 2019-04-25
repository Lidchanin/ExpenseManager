using BackendlessAPI.Data;
using BackendlessAPI.Exception;
using BackendlessAPI.Persistence;
using ExpenseManager.Helpers;
using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public class ServiceClient : IServiceClient
    {
        #region Instance

        private static ServiceClient _instance;

        public static readonly ServiceClient Instance = _instance ?? (_instance = new ServiceClient());

        #endregion

        #region ApplicationId and ApiKeys

        public const string ApplicationId = "81017193-8323-8622-FF92-E4A1848F3700";
        public const string DotNetApiKey = "8AE846DF-06ED-154E-FFA9-3DD743CF2400";
        public const string AndroidApiKey = "FE0E7743-7626-C758-FF3D-A63DF26D9D00";
        public const string IOSApiKey = "C9AACAA4-6A04-B956-FF2D-0CE5D50D5200";

        #endregion ApplicationId and ApiKeys

        private readonly IDataStore<Category> _categoryDataStore;
        private readonly IDataStore<Expense> _expenseDataStore;

        public ServiceClient()
        {
            _categoryDataStore = BackendlessAPI.Backendless.Data.Of<Category>();
            _expenseDataStore = BackendlessAPI.Backendless.Data.Of<Expense>();
        }

        #region Categories operations

        public Task<CommonResponse<IList<Category>>> GetCategories() =>
            ExecuteWithGeneralExceptionHandling(() =>
            {
                var task = _categoryDataStore.FindAsync(DataQueryBuilder.Create());

                return task.ContinueWith(resultTask => resultTask.Result);
            });

        public Task<CommonResponse<Category>> AddCategory(Category category) =>
            ExecuteWithGeneralExceptionHandling(() => _categoryDataStore.SaveAsync(category));

        public Task<CommonResponse<Category>> UpdateCategory(Category category) => 
            AddCategory(category);

        public Task<CommonResponse<long>> DeleteCategory(Category category) =>
            ExecuteWithGeneralExceptionHandling(() => _categoryDataStore.RemoveAsync(category));

        #endregion Categories operations

        #region Expenses operations

        public Task<CommonResponse<IList<Expense>>> GetExpensesForAllTime() =>
            ExecuteWithGeneralExceptionHandling(() =>
            {
                var task = _expenseDataStore.FindAsync(DataQueryBuilder.Create());

                return task.ContinueWith(resultTask => resultTask.Result);
            });

        public Task<CommonResponse<Expense>> AddExpense(Expense expense) =>
            ExecuteWithGeneralExceptionHandling(() => _expenseDataStore.SaveAsync(expense));

        public Task<CommonResponse<Expense>> UpdateExpense(Expense expense) =>
            AddExpense(expense);

        public Task<CommonResponse<long>> DeleteExpense(Expense expense) =>
            ExecuteWithGeneralExceptionHandling(() => _expenseDataStore.RemoveAsync(expense));

        #endregion Expenses operations

        #region private methods

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
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.RequestCanceled)
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
                response.Message = ex.Message;
            }
            catch (BackendlessException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion private methods
    }
}