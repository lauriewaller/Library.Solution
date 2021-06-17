using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()
    {
      this.JoinEntities = new HashSet<AuthorBook>();
      this.Copies = new HashSet<Copy>();      
    }

    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }

    // public int CopyId { get; set; } 
    // public virtual Copy Copy { get; set; }

    //public virtual ApplicationUser User { get; set; } //new line
    public virtual ICollection<Copy> Copies { get; set; }

    public virtual ICollection<AuthorBook> JoinEntities { get; } //a collection navigation property for JoinEntities, which will hold the list of relationships this Book is a part of -- which is how we will find its related Categories. Note that this property only has a getter method while the collection navigation property on the Author class has both a getter and setter. ***We could add a setter method to the JoinEntities property as well but we'll only be modifying the relationship between an Book and a Author via a Author's JoinEntities in our application.*** 
                                                                 // Note that the model's JoinEntities property now holds the list of associated categories. 
  }
}
