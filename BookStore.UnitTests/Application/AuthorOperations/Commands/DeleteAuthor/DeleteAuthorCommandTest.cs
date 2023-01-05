using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }
        //Silinmesi istenen yazar db de yoksa çalışacak kod
        [Fact]
        public void WhenDataNoForIdInDb_InvalidOperationExeption_SholudBeReturn()
        {
            DeleteAuthorCommand cmd = new DeleteAuthorCommand(_context);
            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz Id'e ait yazar yok");

        }

        //Silmek istediğimiz yazarın yazyında kitabı varsa çalışacak kod
        [Fact]
        public void WhenIfToAuthorToBeDeletedHasABookInPublication_InvalidOperationExeption_ShouldBeReturn()
        {
            var book = new Book()
            {
                Title = "TestBook",
                PageCount = 350,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
                AuthorId = 100000
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            var author = new Author()
            {
                Id = 100000,
                Name = "Test",
                SurName = "Test",
                BirthDate = new DateTime(2000, 09, 09)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var cmd = new DeleteAuthorCommand(_context);
            cmd.AuthorIdDto = author.Id;
            FluentActions.Invoking(() => cmd.Handle())
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz yazarın yayında kitabı var önce kitabı silmelisiniz");



        }

        // silmek istediğimiz yazar db de varsa ve yayında kitabı yoksa çalışacak olan kod
        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeDeleted()
        {
            var author = new Author()
            {
                Name = "Test",
                SurName = "Test",
                BirthDate = new DateTime(2000, 09, 09)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var cmd = new DeleteAuthorCommand(_context);
            cmd.AuthorIdDto = author.Id;

            FluentActions.Invoking(() => cmd.Handle()).Invoke();

            var authorChecker= _context.Authors.FirstOrDefault(x=>x.Id ==author.Id);   
            authorChecker.Should().BeNull();
        }
    }
}
