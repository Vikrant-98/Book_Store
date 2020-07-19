using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class UserRegistrationResponse
    {
        public int UserID { get; set; }
        //First Name
        public string FirstName { get; set; }
        //Last Name
        public string LastName { get; set; }
        //Mail ID
        public string EmailID { get; set; }
        //User Categiry
        public string UserCategory { get; set; }
        //CreateDate
        public DateTime CreatedDate { get; set; }
        //ModifiedDate
        public DateTime ModifiedDate { get; set; }
    }
}
