using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class PlaceOrderResponce
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public int CartId { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public int Price { get; set; }

        public int Pages { get; set; }

        public bool IsPlace { get; set; }
    }
}
