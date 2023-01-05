using BookStore.Application.BookOperations.Command.DeleteBooks;
using FluentAssertions;

namespace BookStore.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError()
        {
            //Arrange
            DeleteBooks cmd = new DeleteBooks(null);
            cmd.BookId = 0;

            //Act
            DeleteBookValidator vld = new DeleteBookValidator();
            var result = vld.Validate(cmd);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrannge
            DeleteBooks cmd = new DeleteBooks(null);
            cmd.BookId = 1;

            //Act
            DeleteBookValidator vld = new DeleteBookValidator();
            var result = vld.Validate(cmd);

            //Assert
            result.Errors.Count.Should().Be(0);
                                      //.Equal(0); 
        }
    }
}
