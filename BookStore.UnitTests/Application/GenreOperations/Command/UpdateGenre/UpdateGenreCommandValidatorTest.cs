using BookStore.Application.GenreOperations.Commands.UpdateGener;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError()
        {
            UpdateGenreCommand cmd = new UpdateGenreCommand(null, null);
            cmd.Model= new UpdateGenreModel()
            {
                Name= "f"
            };
            UpdateGenreCommandValidator vld = new UpdateGenreCommandValidator();
            var result= vld.Validate(cmd);
             
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand cmd = new UpdateGenreCommand(null, null);
            cmd.Model = new UpdateGenreModel()
            {
                Name = "fffffff"
            };
            UpdateGenreCommandValidator vld = new UpdateGenreCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }
    }
}
