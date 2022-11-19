using System;
using System.Collections.Generic;

namespace DataFirst.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public double Price { get; set; }

        public Author Author { get; set; }
    }
}
