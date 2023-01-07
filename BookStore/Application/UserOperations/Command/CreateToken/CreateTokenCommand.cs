using AutoMapper;

using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Model;

namespace BookStore.Application.UserOperations.Command.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.
                FirstOrDefault(x => x.Email == Model.Email&& x.Password==Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccesToken(user);

                user.RefreshToken= token.RefreshToken;
                user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("E-Mail yada şifre hatalı");
            }

            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
    public class CreateTokenModel
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

    }
}
