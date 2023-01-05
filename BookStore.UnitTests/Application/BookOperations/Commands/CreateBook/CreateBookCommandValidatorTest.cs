using BookStore.Application.BookOperations.Command.CreateBook;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Command.CreateBook.CreateBooksCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("eeeeeeee",0,0,0)]
        [InlineData("eeeeeeee",1,0,0)]
        [InlineData("eeeeeeee",0,1,0)]
        [InlineData("eeeeeeee",0,0,1)]
        [InlineData("eeeeeeee",0,1,1)]
        [InlineData("",0,0,0)]
        [InlineData("",1,0,0)]
        [InlineData("",0,1,0)]
        [InlineData("",0,0,1)]
        [InlineData("eee",1,1,1)]
        [InlineData(" ",0,0,0)]
        [InlineData("eeeeeeee",1,0,1)]
        [InlineData("",1,0,1)]
        [InlineData("",1,1,1)]
        public void  WhenInvalidInputAreGiven_Validator_SholudBeReturnError(string title,int paceCount,int genreId,int authorId)
        {
            // arrange
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBookModel()
            {
                
                Title = title,
                PageCount = paceCount,
                PublishDate = DateTime.Now.Date.AddYears(-4),
                GenreId = genreId,
                AuthorId = authorId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result= validator.Validate(command);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
         public void WhenDateTimeEqualNowIsGiven_Validator_SholudNotBeReturnError()
        {
            // arrange
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "hhhhhhh",
                PageCount = 123,
                PublishDate = DateTime.Now.Date.AddYears(5),
                GenreId = 1,
                AuthorId = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }[Fact]
         public void WhenValidInputAreGiven_Validator_SholudNotBeReturnError()
        {
            // arrange
            CreateBooksCommand command = new CreateBooksCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "hhhhhhh",
                PageCount = 123,
                PublishDate = DateTime.Now.Date.AddYears(-6),
                GenreId = 1,
                AuthorId = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
