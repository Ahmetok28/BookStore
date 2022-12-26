using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public readonly int Id;



        public GetBookByIdQuery(BookStoreDbContext dbContext, int id, IMapper mapper )
        {
            _dbContext = dbContext;
            Id = id;
            _mapper = mapper;
        }

       

        public BookViewModelById Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == Id).Single();
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
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}
