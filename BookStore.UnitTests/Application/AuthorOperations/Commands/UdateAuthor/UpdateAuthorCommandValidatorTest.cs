
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UdateAuthor
{
    public class UpdateAuthorCommandValidatorTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Test", "")]
        [InlineData("", "Test")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(string name, string surName)
        {
            UpdateAuthorCommand cmd = new UpdateAuthorCommand(null, null);
            cmd.Model = new UpdateAuthorModel()
            {
                Name = name,
                SurName = surName,
                BirthDate = DateTime.Now.Date.AddYears(-40)
            };

            UpdateAuthorCommandValidator vld = new UpdateAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand cmd = new UpdateAuthorCommand(null, null);
            cmd.Model = new UpdateAuthorModel()
            {
                Name = "Test",
                SurName = "Test",
                BirthDate = DateTime.Now.Date.AddYears(5)
            };

            UpdateAuthorCommandValidator vld = new UpdateAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand cmd = new UpdateAuthorCommand(null, null);
            cmd.Model = new UpdateAuthorModel()
            {
                Name = "Test",
                SurName = "Test",
                BirthDate = DateTime.Now.Date.AddYears(-40)
            };

            UpdateAuthorCommandValidator vld = new UpdateAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }
    }
}
