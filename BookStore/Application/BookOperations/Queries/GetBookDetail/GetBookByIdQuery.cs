using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookByIdQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookViewModelById Model { get; set; }
        public int BookId;



        public GetBookByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
           
            _mapper = mapper;
        }



        public BookViewModelById Handle()
        {
            var book = _dbContext.Books
                //.Include(x=>x.Genre)
                //.Include(x => x.Authors)
                .Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Bu İd' ye Sahip Bir kitap Yok ");
            BookViewModelById vm = _mapper.Map<BookViewModelById>(book);
            return vm;
        }



        public class BookViewModelById
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
        }
    }
}
