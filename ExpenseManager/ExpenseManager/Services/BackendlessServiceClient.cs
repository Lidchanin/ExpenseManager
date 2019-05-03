using ExpenseManager.Extensions;
using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Services
{
    public sealed class BackendlessServiceClient : BaseServiceClient, IBackendlessServiceClient
    {
        #region Instance

        private static BackendlessServiceClient _instance;

        public static readonly BackendlessServiceClient Instance = _instance ?? (_instance = new BackendlessServiceClient());

        #endregion Instance

        #region ApplicationId and ApiKeys

        private const string ApplicationId = "81017193-8323-8622-FF92-E4A1848F3700";
        private const string DotNetApiKey = "8AE846DF-06ED-154E-FFA9-3DD743CF2400";
        private const string RestApiKey = "5A6AF534-4AB4-F5F7-FF87-6F6B536AD500";
        private const string AndroidApiKey = "FE0E7743-7626-C758-FF3D-A63DF26D9D00";
        private const string IOSApiKey = "C9AACAA4-6A04-B956-FF2D-0CE5D50D5200";

        #endregion ApplicationId and ApiKeys

        #region Request support strings

        private const string WhereKeyword = "where=";
        private const string SortByKeyword = "sortBy=";
        private const string AndKeyword = "%20AND%20";
        private const string AscKeyword = "%20asc";
        private const string DescKeyword = "%20desc";

        #endregion Request support strings

        private const string DataServicePrefix = "data";
        private const string UserServicePrefix = "users";

        private const string ExpensesTableName = "Expenses";

        private BackendlessServiceClient() : base(new HttpClient(new LogHandler(new HttpClientHandler()))
        {
            BaseAddress = new Uri($"https://api.backendless.com/{ApplicationId}/{RestApiKey}/"),
            Timeout = TimeSpan.FromMinutes(1.5)
        })
        {
        }

        public async Task<CommonResponse<List<Expense>>> GetExpensesForAllTime() =>
            await GetAsync<List<Expense>>($"{DataServicePrefix}/{ExpensesTableName}");

        public async Task<CommonResponse<List<Expense>>> GetExpensesForMonthAndYear(int month, int year,
            bool sortedByAsc = false)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException();

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.ToLastDateOfMonth();

            var startMillis = startDate.ToUnixMillis();
            var endMillis = endDate.ToUnixMillis();

            var sb = new StringBuilder(120);
            sb.Append(DataServicePrefix).Append("/").Append(ExpensesTableName)
                .Append("?")
                .Append(WhereKeyword)
                .Append(Expense.TimestampColumn).Append(">=").Append(startMillis)
                .Append(AndKeyword)
                .Append(Expense.TimestampColumn).Append("<=").Append(endMillis)
                .Append("&")
                .Append(SortByKeyword).Append(Expense.TimestampColumn).Append(sortedByAsc ? AscKeyword : DescKeyword);

            return await GetAsync<List<Expense>>(sb.ToString());
        }

        public async Task<CommonResponse<Expense>> AddExpense(Expense expense) =>
            await PostAsync<Expense, Expense>($"{DataServicePrefix}/{ExpensesTableName}", expense);

        public async Task<CommonResponse<Expense>> UpdateExpense(Expense expense) =>
            await PutAsync<Expense, Expense>($"{DataServicePrefix}/{ExpensesTableName}/{expense.Id}", expense);

        public async Task<CommonResponse<long>> DeleteExpense(string expenseId) =>
            await DeleteAsync<long>($"{DataServicePrefix}/{ExpensesTableName}/{expenseId}");
    }
}