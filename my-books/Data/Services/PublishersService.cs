using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Security.Policy;
using Publisher = my_books.Data.Models.Publisher;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context) 
        { 
            _context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name,
            };
            _context.Add(_publisher);
            _context.SaveChanges();
        }
    }
}
