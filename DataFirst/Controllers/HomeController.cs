using DataFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFirst.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext _context;
        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public string Index()

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
       
    
    }
}
