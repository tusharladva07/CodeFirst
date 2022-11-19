using CodeFirst.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace CodeFirst.Data
{
    public class DbInitialize
    {
        public static void Initialize(LibraryContextDb context)
        {
            context.Database.EnsureCreated();

            if(!context.Author.Any())
            {   
                //Add default Auhtor data
                var authors = new List<Author>()
                {
                    new Author{ Name = "first author",MailId="first@gmail.com"},
                    new Author{ Name = "second author",MailId="second@gmail.com"},
                    new Author{ Name = "third author",MailId="third@gmail.com"},
                    new Author{ Name = "four author",MailId="four@gmail.com"}
                };
                context.Author.AddRange(authors);
                context.SaveChanges();
            }
            if (!context.Book.Any())
            {
                //Add default Auhtor data
                var books = new List<Book>()
                {
                    new Book{ AuthorId =1,Title="First Book",Description="first book description",Price=150 },
                    new Book{ AuthorId =2,Title="Second Book",Description="second book description",Price=250 },
                    new Book{ AuthorId =3,Title="Third Book",Description="third book description",Price=350 },
                    new Book{ AuthorId =3,Title="Four Book",Description="four book description",Price=400 }
                };
                context.Book.AddRange(books);
                context.SaveChanges();
            }

        }
    }
}
