using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Query.GetAuthors
{
    public class GetAuthorQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x=>x.Id);
            var returnList = _mapper.Map<List<AuthorViewModel>>(authors);
            return returnList;
        }

        public class AuthorViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
           
            public DateTime BirthDate { get; set; }
        }
    } 
}

