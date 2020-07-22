using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Responce
{
    public class BooksResponse
    {
        public int BookId { get; set; }

        public int AdminId { get; set; }

        public int Price { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public string Image { get; set; }

        public int Pages { get; set; }

        public string Description { get; set; }

        public int Available { get; set; }

        public bool IsDeleted { get; set; }
    }
}
