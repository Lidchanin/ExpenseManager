using Weborb.Service;

namespace ExpenseManager.Models
{
    public class Category
    {
        [SetClientClassMemberName("objectId")]
        public string Id { get; set; }

        [SetClientClassMemberName("name")]
        public string Name { get; set; }
    }
}