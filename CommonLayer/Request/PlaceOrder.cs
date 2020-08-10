using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Request
{
    public class PlaceOrder
    {
        public int CartId { get; set; }

        public int Quantity { get; set; }

        public int AddressID { get; set; }
    }
    public class CalcelOrder
    {
        public int OrderId { get; set; }

    }
}
