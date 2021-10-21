using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public byte? CategoryId { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
