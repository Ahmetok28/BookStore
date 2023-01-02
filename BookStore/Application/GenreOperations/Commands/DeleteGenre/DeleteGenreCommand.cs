using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id==GenreId); 
            if (genre != null)
            {
                throw new InvalidCastException("kitap Türü Bulunamadı");
            }
            _context .Genres.Remove(genre);
            _context.SaveChanges();
        }

    }
}
