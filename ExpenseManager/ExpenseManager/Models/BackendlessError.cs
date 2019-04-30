using Newtonsoft.Json;

namespace ExpenseManager.Models
{
    public class BackendlessError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}