using BookStore.DBOperations;

namespace BookStore.Application.BookOperations.Command.DeleteBooks
{
    public class DeleteBooks
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId;

        public DeleteBooks(IBookStoreDbContext dbContext )
        {
            _dbContext = dbContext;
            
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            
            if (book is null)
                throw new InvalidOperationException("Silmek İstediğininiz Id'e Sahip Kitap Yok");
            
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }
}
