using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Query.GetAuthors;
using BookStore.Application.AuthorOperations.Query.GetAuthorsDetail;

using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController:ControllerBase
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public AuthorController(BookStoreDbContext _context, IMapper _mapper )
        {
            context = _context;
            mapper = _mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            GetAuthorQuery query = new GetAuthorQuery(context,mapper);
            var obj = query.Handle();
            return Ok(obj); 
        }
        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(context,mapper);
            query.AuthorId = id;
            GetAuthorsDetailQueryValidator validations = new GetAuthorsDetailQueryValidator();
            validations.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor )
        {
            CreateAuthorCommand command= new CreateAuthorCommand(context,mapper);
            command.Model= newAuthor;

            CreateAuthorCommandValidator validations = new CreateAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command= new UpdateAuthorCommand(context,mapper);
            command.AuthorId = id;
            command.Model = updateAuthor;

            UpdateAuthorCommandValidator validationRules = new UpdateAuthorCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(context);
            command.AuthorIdDto =id;
            DeleteAuthorCommandValidator validations = new DeleteAuthorCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }
        

        

    }
}
