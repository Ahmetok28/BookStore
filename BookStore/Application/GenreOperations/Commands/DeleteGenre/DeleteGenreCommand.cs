using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id==GenreId); 
            if (genre == null)
            {
                throw new InvalidOperationException("Silmek istediğiniz kitap türü DB'de bulunamadı");
            }
            _context .Genres.Remove(genre);
            _context.SaveChanges();
        }

    }
}
