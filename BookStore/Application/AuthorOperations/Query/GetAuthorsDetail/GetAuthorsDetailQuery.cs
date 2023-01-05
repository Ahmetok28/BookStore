using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Query.GetAuthorsDetail
{
    public class GetAuthorsDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorsDetailQuery(IBookStoreDbContext context , IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var author= _context.Authors.SingleOrDefault(x=> x.Id==AuthorId);
            if (author == null)
            {
                throw new InvalidOperationException("Yazar bulunamadı");
            }
            AuthorDetailViewModel returnAuthor = _mapper.Map<AuthorDetailViewModel>(author);
            return returnAuthor;
        }

        public class AuthorDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
          
            public DateTime BirthDate { get; set; }
        }

    }
}
