using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.DBOperations
{
    public class BookStoreDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }
        //public BookStoreDbContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // in memory database used for simplicity, change to a real db for production applications
        //    options.UseInMemoryDatabase("BookStoreDB");
        //}
        public DbSet<Book> Books { get; set; } = null;

    }
}
