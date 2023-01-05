using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.DBOperations
{
    public class BookStoreDbContext :DbContext,IBookStoreDbContext
    {
        protected readonly IConfiguration Configuration;
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
    
        public DbSet<Book> Books { get; set; } 
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public override int SaveChanges()
        {
           return base.SaveChanges();
        }
    }
}
