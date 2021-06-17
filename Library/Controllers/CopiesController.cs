using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;  // allows us to authorize users
using Microsoft.AspNetCore.Identity; // allows this controller to interact with users from the db
using System.Threading.Tasks; // allows us to call async methods
using System.Security.Claims; // allows claim based authorization
using System;

namespace Library.Controllers
{
  //[Authorize] //This allows access to the BooksController only if a user is logged in. We'll add this attribute to a controller whenever we want to limit its access to signed-in users. This is just one application of authorization.
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; ////We need an instance of UserManager to work with signed-in users. 

    public CopiesController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db; //We also include a constructor to instantiate private readonly instances of the database and the UserManager.
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Details(int id)
    {
      Copy thisCopy = _db.Copies.FirstOrDefault(copy => copy.CopyId == id);
      return View(thisCopy);
    }

    // public ActionResult Index()
    // {
    //   List<Book> model = _db.Books.ToList();
    //   return View(model);
    // }

    // public ActionResult Create()
    // {
    //   return View();
    // }

    // [HttpPost]
    // public ActionResult Create(Book book, int AuthorId)
    // {
    //   // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   // var currentUser = await _userManager.FindByIdAsync(userId);
    //   // book.User = currentUser;
    //   _db.Books.Add(book);
    //   _db.SaveChanges();
    //   // if (AuthorId != 0)
    //   // {
    //   //   _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
    //   // }
    //   // _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // public ActionResult Delete(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   return View(thisBook);
    // }

    // [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   _db.Books.Remove(thisBook);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public ActionResult DeleteAuthor(int joinId)
    // {
    //   var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
    //   _db.AuthorBook.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // public ActionResult Edit(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
    //   return View(thisBook);
    // }

    // [HttpPost]
    // public ActionResult Edit(Book book, int AuthorId)
    // {
    //   if (AuthorId != 0)
    //   {
    //     _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
    //   }
    //   _db.Entry(book).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Details", new { id = book.BookId });
    // }

    // public ActionResult AddAuthor(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
    //   return View(thisBook);
    // }

    // [HttpPost]
    // public ActionResult AddAuthor(Book book, int AuthorId)
    // {
    //   if (AuthorId != 0)
    //   {
    //     if (_db.AuthorBook.Any(join => join.AuthorId == AuthorId && join.BookId == book.BookId) == false) _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
    //   }
    //   _db.SaveChanges();
    //   return RedirectToAction("Details", new { id = book.BookId});
    // }

    // public ActionResult AddCopies(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   return View(thisBook);
    // }

    // [HttpPost]
    // public ActionResult AddCopies(int BookId, Copy copy, string CopyNumber, string Title)
    // {
    //   var convertedCopyNumber = Convert.ToInt32(CopyNumber);
    //   for (int i = 0; i < convertedCopyNumber; i ++)
    //   {
    //     copy.BookTitle = Title;
    //     _db.Copies.Add(copy);
    //     _db.SaveChanges();
    //     copy.CopyId = copy.CopyId + 1;
    //   }

    //   return RedirectToAction("Index");
    // }
  }
}

