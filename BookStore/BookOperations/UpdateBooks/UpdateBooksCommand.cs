using BookStore.DBOperations;

namespace BookStore.BookOperations.UpdateBooks
{
    public class UpdateBooksCommand
    {
        public UpdateBookModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly int _Id;

        public UpdateBooksCommand(BookStoreDbContext dbContext , int id)
        {
            _dbContext = dbContext;
            _Id = id;

        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == _Id);
            if (book is null)
                throw new InvalidOperationException("Bu İd' e Sahip Bir kitap Yok ");


            book.Title = Model.Title !=default ? Model.Title : book.Title;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

            
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
