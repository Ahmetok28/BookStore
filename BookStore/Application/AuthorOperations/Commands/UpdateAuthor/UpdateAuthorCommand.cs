using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public UpdateAuthorCommand(IBookStoreDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper= mapper; 
           
        }
        public void Handle()
        {
            var author =_context.Authors.SingleOrDefault(x=>x.Id== AuthorId);    
            if(author == null)
            {
                throw new InvalidOperationException("Bu ID'e sahip bir yazar yok");
            }
            if (_context.Authors.Any(x =>x.Name == Model.Name && x.SurName==Model.SurName))
            {
                throw new InvalidOperationException("Aynı isimli bir Yazar zaten mevcuttur.");
            }
            _mapper.Map<UpdateAuthorModel, Author>(Model, author);

            //author.Name= Model.Name != default ? Model.Name:author.Name;
            //author.SurName= Model.SurName != default ? Model.SurName : author.SurName;
            //author.BirthDate= Model.BirthDate != default ? Model.BirthDate : author.BirthDate;
            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }      
        public DateTime BirthDate { get; set; }

        public static implicit operator UpdateAuthorModel(CreateAuthorModel v)
        {
            throw new NotImplementedException();
        }
    }
}
