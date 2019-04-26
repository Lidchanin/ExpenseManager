using System;
using Weborb.Service;

namespace ExpenseManager.Models
{
    public class Expense
    {
        [SetClientClassMemberName("objectId")]
        public string Id { get; set; }

        [SetClientClassMemberName("name")]
        public string Name { get; set; }

        [SetClientClassMemberName("cost")]
        public double Cost { get; set; }

        [SetClientClassMemberName("timestamp")]
        public DateTime Timestamp { get; set; }

        [SetClientClassMemberName("category")]
        public int Category { get; set; }
    }
}