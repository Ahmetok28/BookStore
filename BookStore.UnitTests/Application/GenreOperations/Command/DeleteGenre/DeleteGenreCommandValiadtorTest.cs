using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandValiadtorTest
    {

        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError()
        {
            DeleteGenreCommand cmd = new DeleteGenreCommand(null);
            cmd.GenreId = 0;

            DeleteGenreCommandValidator vld = new DeleteGenreCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteGenreCommand cmd = new DeleteGenreCommand(null);
            cmd.GenreId = 1;

            DeleteGenreCommandValidator vld = new DeleteGenreCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }
    }
}
