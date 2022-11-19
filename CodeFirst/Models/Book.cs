using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("AllBooks")]
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }

       // [RegularExpression("[a-zA-Z0-9]",ErrorMessage = "enter alphanumeric value only")]
        //[StringLength(20, ErrorMessage ="Book title must be 20 < charcters")]
    //[StringLength(50, ErrorMessage = "Book title must be 50 < charcters")]
        public string Description { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Range(100,600)]
        public double Price { get; set; }
        public Author Author { get; set; }  
        public DateTime PublishedDate{ get; set; }  
        

    }
}
