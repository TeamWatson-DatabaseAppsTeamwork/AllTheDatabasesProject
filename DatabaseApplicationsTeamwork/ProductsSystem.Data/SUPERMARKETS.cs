//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductsSystem.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUPERMARKETS
    {
        public SUPERMARKETS()
        {
            this.PRICES = new HashSet<PRICES>();
        }
    
        public decimal ID { get; set; }
        public string LOCATION { get; set; }
    
        public virtual ICollection<PRICES> PRICES { get; set; }
    }
}
