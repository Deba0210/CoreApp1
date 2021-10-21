using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
