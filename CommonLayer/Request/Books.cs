using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Request
{
    public class Books
    {
        [Required(ErrorMessage = "BookName Is Required")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "AuthorName Is Required")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pages Is Required")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Available Is Required")]
        public int Available { get; set; }

       
    }
}
