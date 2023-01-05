using AutoMapper;
using AutoMapper.Execution;
using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _dbContext;

        public readonly IMapper _mapper;

        public GetGenresQuery(IMapper mapper, IBookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<GenresViewModel> Handle()
        {
            var Genres= _dbContext.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(Genres);
            return returnObj;

        }

        public class GenresViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


    }
}
