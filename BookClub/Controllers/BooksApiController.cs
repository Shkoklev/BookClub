using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookClub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookClub.Controllers
{
    public class BooksApiController : ApiController
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        public BooksApiController()
        {
            this.db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: api/BooksApi
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult RateBook(int id, int value)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var book = db.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            AddUserRatingToBook(book, user, value);

            if (!CheckDuplicate(book, user))
            {
                AddAssociation(book, user);
            }

            db.SaveChanges();

            return Ok();
        }

        public void AddUserRatingToBook(Book book,ApplicationUser user, int value)
        {
            var UserBook = db.UserBookRatings.FirstOrDefault(ub => ub.BookId == book.Id && ub.UserId == user.Id);

            if(UserBook != null)
            {
                book.Rating -= UserBook.Value;
                book.Rating += value;
                UserBook.Value = value;
                book.AverageRating = (float)Math.Round(Convert.ToDecimal((float)book.Rating / book.Votes), 1);
            }

            else
            {
                UserBookRatingModel model = new UserBookRatingModel();
                model.UserId = user.Id;
                model.BookId = book.Id;
                model.Value = value;
                db.UserBookRatings.Add(model);
                UpdateBookRating(book, value);
            }
           
        }

        public void UpdateBookRating(Book book, int value)
        {
            book.Rating += value;
            book.Votes += 1;
            book.AverageRating = (float)Math.Round(Convert.ToDecimal((float)book.Rating / book.Votes), 1);
        }

        public bool CheckDuplicate(Book book, ApplicationUser user)
        {
            return user.Books.Contains(book) && book.Users.Contains(user);
        }

        public void AddAssociation(Book book, ApplicationUser user)
        {
            user.Books.Add(book);
            book.Users.Add(user);
        }

        // GET: api/BooksApi/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/BooksApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BooksApi
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, book);
        }

        // DELETE: api/BooksApi/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}