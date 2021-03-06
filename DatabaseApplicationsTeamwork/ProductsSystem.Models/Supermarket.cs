﻿namespace ProductsSystem.Models
{
    using System.Collections.Generic;

    public class Supermarket
    {
        public Supermarket()
        {
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
