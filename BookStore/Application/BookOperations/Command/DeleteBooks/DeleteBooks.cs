using BookStore.DBOperations;

namespace BookStore.Application.BookOperations.Command.DeleteBooks
{
    public class DeleteBooks
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId;

        public DeleteBooks(BookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
            
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            
            if (book is null)
                throw new InvalidOperationException("Silmek İstediğininiz Id'e Sahip Kitap Yok");
            //if (_dbContext.Authors.SingleOrDefault(x => x.Id == book.Author.Id))
            //{
            //    var b = _dbContext.Authors.SingleOrDefault(x => x.Id ==book.Author.Id);
            //    b.IsPublishing = false;
            //}
            //book.Author.IsPublishing = bool.FalseString(book.Author.IsPublishing ? book.Author.IsPublishing : false);
            //var author = _dbContext.Authors.SingleOrDefault(x => x.)
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
