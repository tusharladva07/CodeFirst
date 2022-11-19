using System;
using System.Collections.Generic;

namespace DataFirst.Models
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
