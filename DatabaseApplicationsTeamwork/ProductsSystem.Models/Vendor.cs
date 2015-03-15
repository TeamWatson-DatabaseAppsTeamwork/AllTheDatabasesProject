namespace ProductsSystem.Models
{
    using System.Collections.Generic;

    public class Vendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } 
    }
}
