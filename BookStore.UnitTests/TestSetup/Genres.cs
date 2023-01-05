using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookStore.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenre(this BookStoreDbContext context)
        {
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
        }
    }
}
