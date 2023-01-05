using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenre();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
        
    }
}
