using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId;



        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
           
            _mapper = mapper;
        }



        public BookViewModelById Handle()
        {
            var book = _dbContext.Books
                .Include(x=>x.Genre)
                .Include(x => x.Authors)
                .Where(book => book.Id == BookId).Single();
            if (book is null)
                throw new InvalidOperationException("Bu İd' ye Sahip Bir kitap Yok ");
            BookViewModelById vm = _mapper.Map<BookViewModelById>(book);//new BookViewModelById
                                                                        //{

            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //        PageCount = book.PageCount

            //};
            return vm;
        }



        public class BookViewModelById
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
        }
    }
}
