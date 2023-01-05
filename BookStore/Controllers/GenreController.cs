using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGener;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.GenreOperations.Queries.GetGenresDetail;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_mapper, _context);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpGet("id")]
        public ActionResult GetGenres(int id)
        {
            GetGenresDetailQuery query = new GetGenresDetailQuery(_mapper, _context);
            query.GenreId = id;
            GetGenresDetailQueryValidator validations = new GetGenresDetailQueryValidator();
            validations.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context,_mapper);
            command.Model = newGenre;

            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
        [HttpPut("id")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context,_mapper);
            command.GenreId = id;
            command.Model= updateGenre;

            UpdateGenreCommandValidator validations= new UpdateGenreCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId=id;
            DeleteGenreCommandValidator validations =new DeleteGenreCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }



    }

    
}
