using AutoMapper;
using BookStore.Application.UserOperations.Command.CreateToken;
using BookStore.Application.UserOperations.Command.CreateUser;
using BookStore.Application.UserOperations.Command.RefreshToken;
using BookStore.DBOperations;
using BookStore.TokenOperations.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

      //public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor )
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command= new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper,_configuration);
            command.Model = login;
            var token = command.Handle();
            return token;

        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> CreateToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;

        }



    }
}
