using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class AddressResponce
    {
        public int AddressID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string UserAddress { get; set; }

        public string PhoneNumber { get; set; }

        public int PinCode { get; set; }

        public string LandMark { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
