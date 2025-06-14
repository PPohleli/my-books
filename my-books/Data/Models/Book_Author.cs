namespace my_books.Data.Models
{
    public class Book_Author //Joining Entity
    {
        public int Id { get; set; }

        //Navigation Properties
        public int BookId { get; set; } //Foriegn key
        public Book Book { get; set; }

        public int AuthorId { get; set; } //Foriegn key
        public Author Author { get; set; }
    }
}
