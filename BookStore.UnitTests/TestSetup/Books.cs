using BookStore.DBOperations;
using BookStore.Entities;
using System;


namespace BookStore.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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

                   );
        }
    }
}
