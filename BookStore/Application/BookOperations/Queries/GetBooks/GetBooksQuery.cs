using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books
                .Include(x => x.Genre)
                .Include(x => x.Authors)
                .OrderBy(x => x.Id).ToList();

            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);
            //new List<BookViewModel>();
            //foreach (var book in bookList)
            //{
            //    vm.Add(new BookViewModel()
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //        PageCount = book.PageCount
            //    });

            //}
            return vm;
        }

        public class BookViewModel
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
