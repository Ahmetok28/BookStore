using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBooks;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBooks;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static BookStore.BookOperations.CreateBook.CreateBooksCommand;
using static BookStore.BookOperations.UpdateBooks.UpdateBooksCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]s")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        // Http Get ----> Tüm Listeyi Getirme
        [HttpGet(Name = "GetBooks")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        // Https GetById ----> İd'ye Göre Getirme
        [HttpGet("{id}", Name = "GetBookById")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context,id);
            var result = query.Handle();
            return Ok(result);
        }

        // Http Post ----> Yeni Oluşturma
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBooksCommand command = new CreateBooksCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }

            return Ok();
        }

        // Http Put ---> Güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBooksCommand command = new UpdateBooksCommand(_context,id);
            try
            {
                command.Model= updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
            

        }
        // Http Delete ----> Silme
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBooks deleteBook = new DeleteBooks(_context,id);
            try
            {
                deleteBook.Handle();
            }
            catch (Exception e)
            {

               return BadRequest(e.Message);
            }
            return Ok();
        }



    }
}
