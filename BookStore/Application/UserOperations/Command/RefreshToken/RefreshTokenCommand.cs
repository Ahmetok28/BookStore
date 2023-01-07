using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Model;

namespace BookStore.Application.UserOperations.Command.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string  RefreshToken { get; set; }
        private readonly IBookStoreDbContext _context;
       
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext context,  IConfiguration configuration)
        {
            _context = context;
           
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.
                FirstOrDefault(x => x.RefreshToken==RefreshToken&&x.RefreshTokenExpireDate>DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccesToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("valid bir refresh tokeen bulunamadı");
            }

          
        }
    }
}
