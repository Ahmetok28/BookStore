using BookStore.Application.BookOperations.Command.UpdateBooks;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Command.UpdateBooks.UpdateBooksCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("eeeeeeee", 0, 0, 0)]
        [InlineData("eeeeeeee", 1, 0, 0)]
        [InlineData("eeeeeeee", 0, 1, 0)]
        [InlineData("eeeeeeee", 0, 0, 1)]
        [InlineData("eeeeeeee", 0, 1, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("eee", 1, 1, 1)]
        [InlineData(" ", 0, 0, 0)]
        [InlineData("eeeeeeee", 1, 0, 1)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 1, 1, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeRetrurn(string Title,int pageCount,int genreId,int authorId)
        {
            //Arrange
            UpdateBooksCommand command = new UpdateBooksCommand(null, null);
            command.Model = new UpdateBookModel()
            {
                Title = Title,
                PageCount = pageCount,
                GenreId = genreId,
                AuthorId = authorId,
                 PublishDate= DateTime.Now.Date.AddYears(-4)
            };

            //Act
            UpdateBooksCommandValidator vld = new UpdateBooksCommandValidator();
            var result = vld.Validate(command);

            // Assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //Arrange
            UpdateBooksCommand command = new UpdateBooksCommand(null, null);
            command.Model = new UpdateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1234,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date.AddYears(5)
            };

            //Act
            UpdateBooksCommandValidator vld = new UpdateBooksCommandValidator();
            var result = vld.Validate(command);

            // Assert

            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange
            UpdateBooksCommand command = new UpdateBooksCommand(null, null);
            command.Model = new UpdateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1234,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-5)
            };

            //Act
            UpdateBooksCommandValidator vld = new UpdateBooksCommandValidator();
            var result = vld.Validate(command);

            // Assert

            result.Errors.Count.Should().Be(0);

        }
    }
}
