using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }
        [Column("PublishPlace")]
        [DataType(DataType.Text)]
        public DateTime PublishOn{ get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
