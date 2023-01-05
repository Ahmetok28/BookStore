using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.BookOperations.Command.DeleteBooks;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        


        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            
        }

        [Fact]
        public void WhenDataNoForId_InvalidOperationExeption_SholudBeReturn()
        {
            // Arrange
            DeleteBooks command = new DeleteBooks(_context);
            // Act and Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek İstediğininiz Id'e Sahip Kitap Yok");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
        {
            //Arrange
            var book = new Book()
            {
                Title = "WhenBookExistInDatabase_Book_ShouldBeDeleted",
                PageCount = 350,
                PublishDate = new DateTime(2000, 10, 10),
                GenreId = 1,
                AuthorId=1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var cmd = new DeleteBooks(_context);
            cmd.BookId = book.Id;

            //Act
            FluentActions.Invoking(() => cmd.Handle()).Invoke();

            //Assert
            var bookChecker = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            bookChecker.Should().BeNull();
        }
    }
}
