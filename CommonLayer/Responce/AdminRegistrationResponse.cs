using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class AdminRegistrationResponse
    {
        public int AdminId { get; set; }
        //First Name
        public string FirstName { get; set; }
        //Last Name
        public string LastName { get; set; }
        //Mail ID
        public string Email { get; set; }
        //User Categiry
        public string UserCategory { get; set; }
        //Address
        public string Address { get; set; }
        //City
        public string City { get; set; }
        //PinCode
        public int PinCode { get; set; }
        //CreateDate
        public DateTime CreatedDate { get; set; }
        //ModifiedDate
        public DateTime ModifiedDate { get; set; }
    }
}
