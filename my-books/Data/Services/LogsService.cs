using my_books.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Services
{
    public class LogsService
    {
        private readonly AppDbContext _context;
        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDB() => _context.Logs.ToList();
    }
}
