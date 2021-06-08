using System.Collections.Generic;

namespace Library.Models
{
  public class Author
    {
        public Author()
        {
            this.JoinEntities = new HashSet<AuthorBook>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; } //new line

        public virtual ICollection<AuthorBook> JoinEntities { get; set; } //( Or does this refer to line 9? a collection navigation property because it contains a reference to many related Items. If we don't have this reference, we won't be able to access related Items in our controllers and views.
    }
}