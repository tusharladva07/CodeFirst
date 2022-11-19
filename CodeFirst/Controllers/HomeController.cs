using CodeFirst.Data;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContextDb _context;

        public HomeController(LibraryContextDb libraryContext)
        {
            _context = libraryContext;
        }
        //public string Migrate()
        //{
        //    _context.Database.Migrate();
        //    return "DB Migrate!!";
        //}
        public string Index()
        {
            _context.Database.EnsureCreated();
            return "DB Created!";

        }
        public string Drop()
        {
            _context.Database.EnsureDeleted();
            return "DB Deleted!";
        }
        public string CreateBook(Book book)
        {
            try
            {
                if (book.AuthorId != 0 && book.Title != null && book.Description != null && book.Price != 0)
                {
                    _context.Add(book);
                    _context.SaveChanges();
                    return "Book Details  Added Successfully";
                }
                else
                {
                    return "Missing some Properties!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ReadBook()

        {
            var books = _context.Book.Include("Author").AsNoTracking();
            StringBuilder sb = new StringBuilder();

            if (books == null)
            {
                return "Not found Any books!";
            }
            else
            {
                foreach (var book in books)
                {
                    sb.Append($"Book Id:{book.Id}\r\n");
                    sb.Append($"Title:{book.Title}\r\n");
                    sb.Append($"Description:{book.Description}\r\n");
                    sb.Append($"Price:{book.Price}\r\n");
                    sb.Append($"Author Id:{book.AuthorId}\r\n");
                    sb.Append($"Name:{book.Author.Name}\r\n");
                    sb.Append($"Mailid:{book.Author.MailId} \r\n");
                    sb.Append("*************************************************************************\r\n");
                }
                return sb.ToString();
            }
        }

        public string BookDetails(int? bookId)
        {
            if (bookId == null)
            {
                return "Enter Book ID !!";
            }
            var result = _context.Book.Include("Author").SingleOrDefaultAsync(b => b.Id == bookId);
            StringBuilder sb = new StringBuilder();
            var book = result.Result;

            if (book == null)
            {
                return "Not found Book with Id!";
            }
            else
            {
                sb.Append($"Title:{book.Title}\r\n");
                sb.Append($"Description:{book.Description}\r\n");
                sb.Append($"Price:{book.Price}\r\n");
                sb.Append($"Author Id:{book.AuthorId}\r\n");
                sb.Append($"Name:{book.Author.Name}\r\n");
                sb.Append($"Mailid:{book.Author.MailId} \r\n");
                sb.Append("*************************************************************************\r\n");
                return sb.ToString();
            }

        }
        public string UpdateBook(int bookId, Book newBook)
        {
            try
            {
                if (bookId != newBook.Id)
                {
                    return "Invalid Data!!";
                }

                else
                {
                    if (newBook.Title != null && newBook.Price > 0 && newBook.Description != null && newBook.AuthorId != 0)
                    {
                        //_context.Update(newBook);
                        //_context.SaveChanges();
                        //return "Book Details Updated Successfully!";


                        var dbbook = _context.Book.SingleOrDefaultAsync(bk => bk.Id == bookId);
                        dbbook.Result.Title = newBook.Title;
                        dbbook.Result.Description = newBook.Description;
                        dbbook.Result.Price = newBook.Price;
                        dbbook.Result.AuthorId = newBook.AuthorId;
                        _context.SaveChanges();
                        return "Book Details Updated Successfully!";
                    }
                    else
                    {
                        return "Missing Some Properties!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteBook(int bookId)
        {
            try
            {
                if (bookId == 0)
                {
                    return "Enter Book id !!";
                }

                else
                {
                    var result = _context.Book.SingleOrDefaultAsync(b => b.Id == bookId);
                    var book = result.Result;
                    if (book == null)
                        return "Book id Does not Exist!";
                    else
                    {
                        _context.Book.Remove(book);
                        _context.SaveChangesAsync();
                        return "Book Deleted Successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
       
        public string CreateAuthor(Author author)
        {
            try
            {
                if (author.Name != null && author.MailId != null)
                {
                    _context.Add(author);
                    _context.SaveChangesAsync();

                    TempData["success"] = "Author Details Added Successfully!";
                    return "Author Details Added Successfully!";
                }
                else
                {
                    TempData["error"] = "Missing Some Properties";
                    return "Missing Some Properties";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string ReadAuthor()  
        {
            var authors = _context.Author.AsNoTracking();
            StringBuilder sb = new StringBuilder();

            if (authors == null)
            {
                return "Not found Any books!";
            }
            else
            {
                foreach (var author in authors)
                {
                    sb.Append($"Author Id:{author.AuthorId}\r\n");
                    sb.Append($"Author Name:{author.Name}\r\n");
                    sb.Append($"Author Mail:{author.MailId}\r\n");
                    sb.Append("*************************************************************************\r\n");
                }
                return sb.ToString();
            }
        }
        public string AuthorDetails(int? authorId)
        {
            if (authorId == null)
            {
                return "Enter Author ID !!";
            }
            var result = _context.Author.SingleOrDefaultAsync(b => b.AuthorId == authorId);
            StringBuilder sb = new StringBuilder();
            var author = result.Result;

            if (author == null)
            {
                return "Not found author with Id!";
            }
            else
            {
                    sb.Append($"Author Id:{author.AuthorId}\r\n");
                    sb.Append($"Author Name:{author.Name}\r\n");
                    sb.Append($"Author Mail:{author.MailId}\r\n");
                    sb.Append("----------------------------------------------------------------\r\n");

                return sb.ToString();
            }

        }
        public string UpdateAuthor(int authorId, Author newAuthor)
        {
            try
            {
                if (authorId != newAuthor.AuthorId)
                {
                    return "Invalid AuthorId!!";
                }

                else
                {
                    if (newAuthor.Name != null && newAuthor.MailId != null && newAuthor.AuthorId != 0)
                    {
                        _context.Update(newAuthor);
                        _context.SaveChanges();
                        return "Author Details Updated Successfully!";


                        //var dbbook = _context.Author.SingleOrDefaultAsync(bk => bk.AuthorId == authorId);
                        //dbbook.Result.Name = newAuthor.Name;
                        //dbbook.Result.MailId= newAuthor.MailId;
                        //dbbook.Result.AuthorId = newAuthor.AuthorId;
                        //_context.SaveChanges();
                        //return "Author Details Updated Successfully!";
                    }
                    else
                    {
                        return "Missing Some Properties!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteAuthor(int authorId)
        {
            try
            {
                if (authorId == 0)
                {
                    return "Enter Author id !!";
                }

                else
                {
                    var result = _context.Author.SingleOrDefaultAsync(b => b.AuthorId == authorId);
                    var author = result.Result;
                    if (author == null)
                        return "Author id Does not Exist!";
                    else
                    {
                        _context.Author.Remove(author);
                        _context.SaveChangesAsync();
                        return "Author Deleted Successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string PrintAsString(Book book)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Book Id:{book.Id}\r\n");
            sb.Append($"Title:{book.Title}\r\n");
            sb.Append($"Description:{book.Description}\r\n");
            sb.Append($"Price:{book.Price}\r\n");
            sb.Append($"Author Id:{book.AuthorId}\r\n");
            sb.Append($"Name:{book.Author.Name}\r\n");
            sb.Append($"Mailid:{book.Author.MailId} \r\n");
            sb.Append("*************************************************************************\r\n");
            return sb.ToString();
        }
        public string SortBookDetails(string ColumnName)
        {
            try
            {


                var books = _context.Book.Include("Author").AsNoTracking();
                if (ColumnName != null)
                {
                    switch (ColumnName.ToLower())
                    {
                        case "id":
                            books = books.OrderBy(b => b.Id);
                            break;

                        case "price":
                            books = books.OrderBy(b => b.Price);
                            break;

                        case "author":
                            books = books.OrderBy(b => b.Author);
                            break;

                        case "title":
                            books = books.OrderBy(b => b.Title);
                            break;

                    }
                }


                StringBuilder data = new StringBuilder();
                foreach (var book in books)
                {
                    data.AppendLine(PrintAsString(book));
                }
                return data.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string FilterBookDetails(string columnName, string value)
        {
            try
            {
                var books = _context.Book.Include("Author").AsNoTracking();

                if (columnName != null && value != null)
                {
                    switch (columnName.ToLower())
                    {
                        case "id":
                            int id = Convert.ToInt32(value);
                            books = books.Where(b => b.Id == id);
                            break;
                        case "price":
                            double price = Convert.ToDouble(value);
                            books = books.Where(b => b.Price == price);
                            break;
                        case "author":
                            books = books.Where(b => b.Author.Name == value);
                            break;
                        case "title":
                            books = books.Where(b => b.Title == value);
                            break;

                        default:
                            break;
                    }
                    StringBuilder data = new StringBuilder();
                    foreach (var book in books)
                    {
                        data.AppendLine(PrintAsString(book));
                    }
                    return data.ToString();
                }
                else
                {
                    return "Missing some Properties";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EntityState()
        {
            var book = new Book()
            {
                Title = "New Book Title",
                Description = "New book description",
                Price = 320,
                AuthorId = 1005
            };
            //_context.Book.Add(book);
            //return _context.Entry(book).State.ToString();
            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
            return "Book Added!!";
        }
        public string AttachEntity(Book book)
        {
            string msg = string.Empty;
            if (book.Id > 0)
            {
                _context.Book.Attach(book);
                _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                msg = "Updated";
            }
            else
            {
                _context.Book.Add(book);
                msg = "Added";
            }
            _context.SaveChanges();
            return $"Book Details {msg} Successfully!!";
        }
        public string EntryState()
        {
            var book = _context.Book.Find(14);
            book.Title = "six book";
            var entry = _context.Entry(book);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Entity State: {entry.State}");
            sb.AppendLine($"Entity Name: {entry.Entity.GetType().FullName}");
            sb.AppendLine($"Original Value: {entry.OriginalValues["Title"]}");
            sb.AppendLine($"Current Value: {entry.CurrentValues["Title"]}");
            return sb.ToString();
        }

        public string NativeStatement()
        {
            //var books = _context.Book.FromSql("SELECT * FROM Book WHERE Price > 200").ToList();
            //StringBuilder sb = new StringBuilder(); 
            //foreach(var book in books)
            //{
            //    sb.Append($"Title:{book.Title} | Description:{book.Description} | Price:{book.Price}\r\n");
            //}
            //return sb.ToString();

            //int result = _context.Database.ExecuteSqlCommand("DELETE FROM Book WHERE Id =14");
            //if(result > 0)
            //{
            //    return "Deleted Successfully!";
            //}
            //else
            //{
            //    return "Not found with given id";
            //}

            DbConnection con = _context.Database.GetDbConnection();
            con.Open();
            DbCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT NAME FROM Author";
            cmd.CommandType = System.Data.CommandType.Text;

            DbDataReader dr = cmd.ExecuteReader();
            StringBuilder data = new StringBuilder();
            while (dr.Read())
            {
                data.AppendLine(dr.GetString(0));
            }
            return data.ToString();
        }
        public string SPDemo(int id)
        {
            try
            {
                //var books = _context.Book.FromSql("GetBooks @Id", new SqlParameter("@Id", id)).ToList();
                //StringBuilder data = new StringBuilder();
                //foreach(var book in books)
                //{
                //    data.AppendLine(book.Title);
                //}
                //return data.ToString();

                if (id <= 0)
                {
                    return "Enter Booik id";
                }
                var con = _context.Database.GetDbConnection();
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "DeleteBooks";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Id", id));

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return "Deleted Successfully!";
                }
                else
                {
                    return "Book with id given does not exist";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        

    

    }
}
