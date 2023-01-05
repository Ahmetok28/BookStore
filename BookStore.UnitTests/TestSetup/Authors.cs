using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}
