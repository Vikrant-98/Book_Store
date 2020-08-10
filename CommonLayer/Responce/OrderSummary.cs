using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class OrderSummary
    {
        public int OrderID { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public string UserAddress { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }
    }
}
