using AutoMapper;

using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.UserOperations.Command.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.
                SingleOrDefault(x => x.Email==Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Aynı E-Mail adresine sahip bir kullanıcı zaten mevcut");
            }
            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

      
    }
    public class CreateUserModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }



    }
}
