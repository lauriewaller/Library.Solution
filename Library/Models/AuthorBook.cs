namespace Library.Models
{
  public class AuthorBook
    {       
        public int AuthorBookId { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public virtual Author Author { get; set; } //we also have both Author and Book included as objects. 
        public virtual Book Book { get; set; }
    }
}