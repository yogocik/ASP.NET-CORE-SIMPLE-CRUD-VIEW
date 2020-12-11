using _11Des2020MVC.DbEntities;
using _11Des2020MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDApplication.Controllers
{
    public class BookController : Controller
    {
        private CRUDContext context;

        public BookController(CRUDContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<BookViewModel> model = context.Set<Books>().ToList().Select(s => new BookViewModel
            {
                Id = s.Id,
                Author = $"{s.Author}",
                Title = $"{s.Title}",
                Published = s.YearPublished
            });
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult AddEditBook(long? id)
        {
            BookViewModel model = new BookViewModel();
            if (id.HasValue)
            {
                Book book = context.Set<Books>().SingleOrDefault(c => c.Id == id.Value);
                if (book != null)
                {
                    model.Id = book.Id;
                    model.Author = book.Author;
                    model.Title = book.Title;
                    model.Year = book.YearPublished;
                }
            }
            return PartialView("~/Views/Books/_AddEditBooks.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddEditBooks(long? id, BookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = !id.HasValue;
                    Books book = isNew ? new Books
                    {
                        AddedDate = DateTime.UtcNow
                    } : context.Set<Books>().SingleOrDefault(s => s.Id == id.Value);
                    book.Id = model.Id;
                    book.Author = model.Author;
                    book.Title = model.Title;
                    book.YearPublished = model.Year;
                    book.ModifiedDate = DateTime.UtcNow;
                    if (isNew)
                    {
                        context.Add(book);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteBooks(long id)
        {
            Books book = context.Set<Books>().SingleOrDefault(c => c.Id == id);
            BookViewModel model = new BookViewModel
            {
                Name = $"{book.Title}"
            };
            return PartialView("~/Views/Books/_DeleteBooks.cshtml", model);
        }
        [HttpPost]
        public IActionResult DeleteBook(long id, FormCollection form)
        {
            Books book = context.Set<Books>().SingleOrDefault(c => c.Id == id);
            context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
