using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get;set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle() 
        {
            var author = _context.Authors.
                SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower()&& x.SurName.ToLower()==Model.SurName.ToLower());
            if(author is not null)
            {
                throw new InvalidOperationException("Yazar Zaten Ekli");
            }
            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }





    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
       
    }
 
}
