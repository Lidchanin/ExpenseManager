using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExpenseManager.Models;

namespace ExpenseManager.Services
{
    public class AzureServiceClient : BaseServiceClient, IAzureServiceClient
    {
        #region Instance

        private static AzureServiceClient _instance;

        public static readonly AzureServiceClient Instance = _instance ?? (_instance = new AzureServiceClient());

        #endregion Instance

        private const string Tables = "tables";
        private const string ExpensesTable = "expenses";

        public AzureServiceClient() : base(new HttpClient(new LogHandler(new HttpClientHandler()))
        {
            BaseAddress = new Uri("https://expense-manager.azurewebsites.net/"),
            DefaultRequestHeaders =
            {
                {"Accept", "application/json"},
                {"ZUMO-API-VERSION", "2.0.0"}
            },
        })
        {
        }

        public async Task<CommonResponse<List<Expense>>> GetExpensesForAllTime() =>
            await GetAsync<List<Expense>>($"{Tables}/{ExpensesTable}");

        public async Task<CommonResponse<Expense>> AddExpense(Expense expense) =>
            await PostAsync<Expense, Expense>($"{Tables}/{ExpensesTable}", expense);
    }
}