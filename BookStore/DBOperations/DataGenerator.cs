using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Books.AddRange(
                   new Book()
                   {
                       Title = "Lean Startup",
                       GenreId = 2, // Personal Growth
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book()
                   {
                       Title = "The Maze Runner",
                       GenreId = 1, // Sci-Fi
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book()
                   {
                       Title = "Lord Of The Rings",
                       GenreId = 3, // Personal Growth
                       PageCount = 200,
                       PublishDate = new DateTime(2001, 06, 12)
                   }
                   
                   );

                context.SaveChanges();
            }
        }

    }
}
