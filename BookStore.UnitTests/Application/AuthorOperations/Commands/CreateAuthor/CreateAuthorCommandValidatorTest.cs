using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest
    {
        [Theory]
        [InlineData("","")]
        [InlineData("Test","")]
        [InlineData("","Test")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(string name, string surName)
        {
            CreateAuthorCommand cmd = new CreateAuthorCommand(null, null);
            cmd.Model = new CreateAuthorModel()
            {
                Name = name,
                SurName = surName,
                BirthDate = DateTime.Now.Date.AddYears(-40)
            };  

            CreateAuthorCommandValidator vld = new CreateAuthorCommandValidator();
            var result= vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand cmd = new CreateAuthorCommand(null, null);
            cmd.Model = new CreateAuthorModel()
            {
                Name = "Test",
                SurName = "Test",
                BirthDate = DateTime.Now.Date.AddYears(5)
            };

            CreateAuthorCommandValidator vld = new CreateAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand cmd = new CreateAuthorCommand(null, null);
            cmd.Model = new CreateAuthorModel()
            {
                Name = "Test",
                SurName = "Test",
                BirthDate = DateTime.Now.Date.AddYears(-40)
            };

            CreateAuthorCommandValidator vld = new CreateAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }

    }
}
