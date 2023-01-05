using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidatorTest
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError()
        {
            CreateGenreCommand cmd = new CreateGenreCommand(null, null);
            cmd.Model = new CreateGenreModel()
            {
                Name = "n"
            };
            CreateGenreCommandValidator vld = new CreateGenreCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand cmd = new CreateGenreCommand(null, null);
            cmd.Model = new CreateGenreModel()
            {
                Name = "qwerty"
            };
            CreateGenreCommandValidator vld = new CreateGenreCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }
    }
}
