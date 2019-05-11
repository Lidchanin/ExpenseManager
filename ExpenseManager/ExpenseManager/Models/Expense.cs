using ExpenseManager.Converters;
using ExpenseManager.Enums;
using Newtonsoft.Json;
using System;

namespace ExpenseManager.Models
{
    public class Expense
    {
        #region Column names in DB
        
        public const string IdColumn = "objectId";
        public const string NameColumn = "name";
        public const string CostColumn = "cost";
        public const string TimestampColumn = "timestamp";
        public const string CategoryColumn = "category";

        #endregion Column names in DB

        [JsonProperty("objectId")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }

        [JsonProperty("timestamp")]
        //[JsonConverter(typeof(LongDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("category")]
        public ExpenseCategories Category { get; set; }
    }
}