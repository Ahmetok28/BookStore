using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Command.UpdateBooks
{
    public class UpdateBooksCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId;

        public UpdateBooksCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Veri Tabanında Böyle bir Kitap Yok");

             
            if (_dbContext.Books.Any(x => x.Title == Model.Title))
            {
                throw new InvalidOperationException("Aynı isime sahip bir kitap zaten mevcut.");
            }
            _mapper.Map<UpdateBookModel, Book>(Model, book);
           


            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {

            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public int AuthorId { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
