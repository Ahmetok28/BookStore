using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context , IMapper mapper)
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
            if (_context.Authors.Where(x=>x.Id !=AuthorId).Select(x=> new {FullName= (x.Name+" "+x.SurName).ToLower()}).Any(x=>x.FullName==Model.SurName))
            {
                throw new InvalidOperationException("Aynı isimli bir Yazar zaten mevcuttur.");
            }
            _mapper.Map<UpdateAuthorModel, Author>(Model, author);

            //author.Name= Model.Name != default ? Model.Name:author.Name;
            //author.SurName= Model.SurName != default ? Model.SurName : author.SurName;
            //author.BirthDate= Model.BirthDate != default ? Model.BirthDate : author.BirthDate;
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string FullName
        {
            get { return Name.ToLower().Trim() + " " + SurName.ToLower().Trim(); }
        }
        public DateTime BirthDate { get; set; }
    }
}
