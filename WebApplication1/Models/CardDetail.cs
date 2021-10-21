using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class CardDetail
    {
        public decimal CardNumber { get; set; }
        public string NameOnCard { get; set; }
        public string CardType { get; set; }
        public decimal Cvvnumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal? Balance { get; set; }
    }
}
