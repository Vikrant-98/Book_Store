using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Request
{
    public class Books
    {
        public string BookName { get; set; }
        
        public string AuthorName { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public int Price { get; set; }

        public int Available { get; set; }
    }
}
