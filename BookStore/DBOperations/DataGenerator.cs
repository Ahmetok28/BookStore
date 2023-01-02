using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "İlber",
                        SurName = "Ortaylı",
                        BirthDate = new DateTime(2001, 06, 12)

                    },
                    new Author
                    {
                        Name = "John Ronald Reuel",
                        SurName = "Tolkien",
                       
                        BirthDate = new DateTime(2001, 06, 12)

                    },
                    new Author
                    {
                        Name = "James",
                        SurName = "Dashner",
                        
                        BirthDate = new DateTime(2001, 06, 12)

                    }

                    );
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Sci-Fi"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }

                    );

                context.Books.AddRange(
                   new Book()
                   {
                       Title = "Bir Ömür Nasıl Yaşanır",
                       GenreId = 1,
                       PageCount = 200,
                       AuthorId = 1,

                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book()
                   {
                       Title = "The Maze Runner",
                       GenreId = 3,
                       PageCount = 200,
                       AuthorId = 2,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book()
                   {
                       Title = "Lord Of The Rings",
                       GenreId = 3,
                       PageCount = 200,
                       AuthorId = 3,
                       
                       PublishDate = new DateTime(2001, 06, 12)
                   }

                   ) ;
                

                context.SaveChanges();
            }
        }

    }
}
