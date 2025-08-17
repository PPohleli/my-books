using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBook() 
        {
            var allBooks = _context.Books.ToList();
            return allBooks;
        }

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(b => b.Id == bookId).Select(n => new BookWithAuthorsVM()
            {
                Title = n.Title,
                Description = n.Description,
                IsRead = n.IsRead,
                DateRead = n.IsRead ? n.DateRead.Value : null,
                Rate = n.IsRead ? n.Rate.Value : null,
                Genre = n.Genre,
                CoverUrl = n.CoverUrl,
                PublisherName = n.Publisher.Name,
                AuthorNames = n.Book_Authors.Select(a => a.Author.FullName).ToList()
            }).FirstOrDefault();
            return _bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM book) 
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _context.SaveChanges();
            }
            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
