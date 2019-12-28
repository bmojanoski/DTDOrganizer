using DTDOrganizer.Models;
using DTDOrganizer.Patterns;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DTDOrganizer.Controllers
{
    //Handles the HTTP requests for the Library module
    [Authorize]
    public class LibraryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Library
        public ActionResult Index()
        {
            var model = new LibraryViewModel
            {
                books = db.BooksModels.Where(b => b.Qty > 0).ToList(),
                courses = db.CoursesModels.ToList(),
                documents = db.DocumentsModels.ToList()
            };
            
            return View(model);
        }
        public ActionResult BookDesc(String isbn)
        {
            var Book = db.BooksModels.Where(book => book.isbn == isbn).First();
            return View(Book);
        }

        // GET: Library/AddBook
        public ActionResult AddBook()
        {
            return View();
        }

        // POST: Library/AddBook
        [HttpPost]
        public ActionResult AddBook(FormCollection collection)
        {
            
            try
            {
                //9780596007126 - Head First Design Patterns
                string isbn = collection["isbn"];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn +"&key=AIzaSyARYJrBPVJ9B77JveSDkwPI5IvWUGVHe1M");
                GoogleBooksApiReader bookInformation = new GoogleBooksApiReader((HttpWebResponse)request.GetResponse());
                if (bookInformation.isValid())
                {
                    BooksModel addBook = new BooksModel(bookInformation);
                    addBook.Qty = int.Parse(collection["qty"]);

                    db.BooksModels.Add(addBook);
                    db.SaveChanges();
                    ViewBag.bookIsbnException = null;
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.bookIsbnException = "Wrong ISBN code!";
                    throw new Exception("Wrong ISBN code");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        // POST: Library/RentABook
        public ActionResult RentABook(string isbn)
        {
            db.BooksModels.First(b => b.isbn.Equals(isbn)).Qty -= 1;
            db.SaveChanges();

            return Json("success");
        }


        // GET: Library/Create
        public ActionResult AddCourse()
        {
            return View();
        }

        // POST: Library/Create
        [HttpPost]
        public ActionResult AddCourse(FormCollection collection)
        {

            try
            {
                CoursesModel newCourse = new CoursesModel
                {
                    name = collection["name"],
                    link = collection["link"]
                };
                db.CoursesModels.Add(newCourse);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        // GET: Library/Create
        public ActionResult AddDocument()
        {
            return View();
        }

        // POST: Library/Create
        [HttpPost]
        public ActionResult AddDocument(DocumentUploadModel document)
        {

            try
            {
                
                DocumentsModel addDocument = new DocumentsModel
                {
                    name = document.fileName
                };
                if (document.File.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(document.File.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Documents"), _FileName);
                    document.File.SaveAs(_path);
                    addDocument.path = "Content/Documents/" + _FileName;
                }

                db.DocumentsModels.Add(addDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }

        //Disposes of the database instance so we can be certain that the database resource is released
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}