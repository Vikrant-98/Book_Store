using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Services
{
    public class User
    {
        
        [Required]
        //[MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid First Name")]
        //First Name
        public string FirstName { get; set; }
        [Required]
        //[MaxLength(100)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2}[a-z]*$", ErrorMessage = "Enter Valid Last Name")]
        //Last Name
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9]{2}[a-zA-Z0-9]*[.]{0,1}[a-zA-Z0-9]*@[a-zA-Z0-9]*.{1}[a-zA-Z0-9]*[.]*[a-zA-Z0-9]*)$", ErrorMessage = "Enter Valid Email")]
        //Mail ID
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(@"^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        //Password
        public string Password { get; set; }
    }
    public class EncryptedPassword
    {
        public static string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
