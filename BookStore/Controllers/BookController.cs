using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBooks;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBooks;
using BookStore.DBOperations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BookOperations.CreateBook.CreateBooksCommand;
using static BookStore.BookOperations.GetBooks.GetBookByIdQuery;
using static BookStore.BookOperations.UpdateBooks.UpdateBooksCommand;
using System.Diagnostics;
using FluentValidation;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]s")]

    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        // Http Get ----> Tüm Listeyi Getirme
        [HttpGet(Name = "GetBooks")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        // Https GetById ----> İd'ye Göre Getirme

        [HttpGet("{id}", Name = "GetBookById")]
        public IActionResult GetById(int id)
        {
            BookViewModelById result;

            try
            {
                GetBookByIdQuery query = new GetBookByIdQuery(_context, id, _mapper);
                GetBookByIdValidator validator= new GetBookByIdValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        // Http Post ----> Yeni Oluşturma
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBooksCommand command = new CreateBooksCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                //if (!result.IsValid)
                //{
                //    foreach (var item in result.Errors)
                //    {
                //        Debug.WriteLine("Özellik " + item.PropertyName + "Error Message: " + item.ErrorMessage);

                //    }
                //}
                //else
                //{
                //    command.Handle();
                //}

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
            UpdateBooksCommand command = new UpdateBooksCommand(_context, id);
            try
            {
                command.Model = updateBook;
                UpdateBooksCommandValidator validator= new UpdateBooksCommandValidator();
                validator.ValidateAndThrow(command);
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
           
            try
            { 
                DeleteBooks deleteBook = new DeleteBooks(_context, id);
                DeleteBookValidator validator= new DeleteBookValidator();
                validator.ValidateAndThrow(deleteBook);
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
