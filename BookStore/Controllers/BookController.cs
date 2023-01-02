using AutoMapper;
using BookStore.DBOperations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.BookOperations.Command.CreateBook.CreateBooksCommand;
using static BookStore.Application.BookOperations.Queries.GetBookDetail.GetBookByIdQuery;
using static BookStore.Application.BookOperations.Command.UpdateBooks.UpdateBooksCommand;
using System.Diagnostics;
using FluentValidation;
using BookStore.Application.BookOperations.Command.CreateBook;
using BookStore.Application.BookOperations.Command.DeleteBooks;
using BookStore.Application.BookOperations.Command.UpdateBooks;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.Application.BookOperations.Queries.GetBooks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = id;
            GetBookByIdValidator validator = new GetBookByIdValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        // Http Post ----> Yeni Oluşturma
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBooksCommand command = new CreateBooksCommand(_context, _mapper);

            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        // Http Put ---> Güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBooksCommand command = new UpdateBooksCommand(_context);
            command.BookId = id;
            command.Model = updateBook;
            UpdateBooksCommandValidator validator = new UpdateBooksCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            return Ok();


        }
        // Http Delete ----> Silme
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBooks deleteBook = new DeleteBooks(_context);
            deleteBook.BookId = id;
            DeleteBookValidator validator = new DeleteBookValidator();
            validator.ValidateAndThrow(deleteBook);
            deleteBook.Handle();

            return Ok();
        }



    }
}
