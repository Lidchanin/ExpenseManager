namespace ExpenseManager.Models
{
    public class CommonResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Content { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}