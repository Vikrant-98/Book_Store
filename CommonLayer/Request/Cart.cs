using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Request
{
    public class Cart
    {
        [Required]
        public int BookID { get; set; }
    }
}
