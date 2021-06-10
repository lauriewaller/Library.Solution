using System.Collections.Generic;
using System;

namespace Library.Models
{
  public class Copy
  {
    public Copy()
    {
      //this.Books = new HashSet<Book>();
    }
    public int CopyId { get; set; }

    public DateTime CheckedOutDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool CheckedOut { get; set; } = false;
    public string BookTitle { get; set; }

    public int BookId { get; set; } 
    public virtual Book Book { get; set; }
    //public virtual ICollection<Book> Books { get; set; }
  }
}


// using System.Collections.Generic;

// namespace UniversityRegistrar.Models
// {
//   public class Course
//   {
//     public Course()
//     {
//       //this.JoinEntities = new HashSet<Enrollment>();
//     }

//     public int CourseId { get; set; }
//     public string Name { get; set; }
//     public int DepartmentId { get; set; }
//     public virtual Department Department { get; set; }

//     public virtual ICollection<Enrollment> JoinEntities { get; }
//   }
// }