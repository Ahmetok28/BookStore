using BookStore.DBOperations;

namespace BookStore.BookOperations.DeleteBooks
{
    public class DeleteBooks
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly int _id;

        public DeleteBooks(BookStoreDbContext dbContext, int id)
        {
            _dbContext= dbContext;
            _id = id;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == _id);
            if (book is null)
                throw new InvalidOperationException("Silmek İstediğininiz Id'e Sahip Kitap Yok");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            
        }
    }
}
