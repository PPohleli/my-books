using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context) 
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks() /*=> _context.Books.ToList();*/
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        public Book GetBookById(int bookId) /*=> _context.Books.FirstOrDefault(n => n.Id == bookId);*/
        {
            var book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            return book;
        }
    }
}
