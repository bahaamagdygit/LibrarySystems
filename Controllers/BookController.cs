using Library.Application.Services;
using Library.Dtos.Book;
using Library.Models;
using Library.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace Library.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        public BookController(IBookService bookService , IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var Books = await _bookService.GetAllPagination(10, 1);
            return View(Books);
        }

       

        // GET: BookController/Create
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var Books = await _authorService.GetAllAuthor();
            ViewBag.Authors = Books.Entities.ToList();

            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateOrUpdateBookDTO Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Res = await _bookService.Create(Book);
                    if (Res.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = Res.Message;
                        return View(Book);
                    }
                }
                else
                {
                    return View(Book);

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                
                var book = await _bookService.GetOne(id);

              
                if (book != null)
                {
                    var authors = await _authorService.GetAllAuthor();
                    ViewBag.Authors = authors.Entities.ToList();

                   
                    return View(book);
                }
                else
                {
                  
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while processing your request.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]
        
        public async Task<ActionResult> Edit(int id, CreateOrUpdateBookDTO Book)
        {

             var authors = await _authorService.GetAllAuthor();
            ViewBag.Authors = authors.Entities.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _bookService.Update(Book);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = result.Message;
                        return View(Book);
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid model state.";
                    return View(Book);
                }
            }
            catch (Exception ex)
            {
               
                ViewBag.Error = "An error occurred while processing your request.";
                return View(Book);
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> HardDelete(int id)
        {
            var prd = await _bookService.GetOne(id);
            return View(prd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> HardDelete(int id, CreateOrUpdateBookDTO Book)
        {
            try
            {
                var p = await _bookService.HardDelete(Book);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Error = "An unexpected error occurred.";
                return View();


            }
        }






        public async Task<IActionResult> AddtoCart(int Id)
        {

           
            if (Id == 0)
            {
                return BadRequest("Invalid list of IDs");
            }

            // هات الكوكي او اعمل وحده جديده
            var cookie = Request.Cookies["BookListIDs"];
            List<int> existingIds = new List<int>();
            if (!string.IsNullOrEmpty(cookie))
            {
                // DeserializeObject بتلغي تسلسل الايديهات
                existingIds = JsonConvert.DeserializeObject<List<int>>(cookie);
            }
            existingIds.Add(Id);

            List<int> uniqueIds = existingIds.Distinct().ToList(); // بتمسح المتكرر

            // بتعيد تسلسل الايديهات مره تاني في الجيسون
            string serializedIds = JsonConvert.SerializeObject(uniqueIds);
            // هنحط الليست دي فالكوكيز
            Response.Cookies.Append("BookListIDs", serializedIds);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SHowCartprouct()
        {
            // بستدعي ليستت الجيسون الي فالكوكي
            var cookie = Request.Cookies["BookListIDs"];
            List<int> existingIds = new List<int>();
            if (!string.IsNullOrEmpty(cookie))
            {
               
                existingIds = JsonConvert.DeserializeObject<List<int>>(cookie);
            }
            var model = await _bookService.GetProductListByIdList(existingIds);

            return View(model);
        }

    }
}
