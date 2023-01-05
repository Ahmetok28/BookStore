using AutoMapper;
using BookStore.DBOperations;
using System;

namespace BookStore.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQuery
    {
        public readonly IBookStoreDbContext _dbContext;

        public readonly IMapper _mapper;
        public int GenreId { get; set; }

        public GetGenresDetailQuery(IMapper mapper, IBookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public GenresDetailViewModel Handle()
        {
            var Genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id==GenreId);
            if (Genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }
            GenresDetailViewModel returnObj = _mapper.Map<GenresDetailViewModel>(Genre);
            return returnObj;

        }

        public class GenresDetailViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}
