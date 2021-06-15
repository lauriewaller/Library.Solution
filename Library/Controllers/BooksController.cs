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
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; ////We need an instance of UserManager to work with signed-in users. 

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db; //We also include a constructor to instantiate private readonly instances of the database and the UserManager.
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //locate the unique identifier for the currently-logged-in user and assign it the variable name userId. this refers to the BookController itself. FindFirst() is a method that locates the first record that meets the provided criteria. This method takes ClaimTypes.NameIdentifier as an argument. We specify ClaimTypes.NameIdentifier to locate the unique ID associated with the current account. NameIdentifier is a property that refers to an Entity's unique ID. The ? is called an existential operator.   if we successfully locate the NameIdentifier of the current user, we'll call Value() to retrieve the actual unique identifier value.
      var currentUser = await _userManager.FindByIdAsync(userId); //First we call the UserManager service that we've injected into this controller.We call the FindByIdAsync() method, which, as its name suggests, is a built-in Identity method used to find a user's account by their unique ID. We provide the userId we just located as an argument to FindByIdAsync(). Thanks to the handy Async suffix in this methods name, we know it runs asynchronously. We include the await keyword so the code will wait for Identity to locate the correct user before moving on.
      var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userBooks);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      book.User = currentUser;
      _db.Books.Add(book);
      _db.SaveChanges();
      // if (AuthorId != 0)
      // {
      //   _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      // }
      // _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
      .Include(book => book.JoinEntities)
      .ThenInclude(join => join.Author)
      .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBook.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = book.BookId });
    }

    public ActionResult AddAuthor(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        if (_db.AuthorBook.Any(join => join.AuthorId == AuthorId && join.BookId == book.BookId) == false) _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = book.BookId});
    }

    public ActionResult AddCopies(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddCopies(int BookId, Copy copy, string CopyNumber)
    {
      var convertedCopyNumber = Convert.ToInt32(CopyNumber);
      for (int i = 0; i < convertedCopyNumber; i ++)
      {
        _db.Copies.Add(copy);
        _db.SaveChanges();
        copy.CopyId = copy.CopyId + 1;
      }

      return RedirectToAction("Index");
    }
  }
}

//Add Author -- get an integer to create copies
//needs book id to connect ot
//one to many relationship 

//post -- need book id 
//add multiples copies to database 
//won't create all properties for copy instances yet
//each copy needs a unique id

//Convert.ToInt32
// result = Convert.ToInt32(value);