using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest
    {
        [Fact]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError()
        {
            DeleteAuthorCommand cmd = new DeleteAuthorCommand(null);
            cmd.AuthorIdDto = 0;

            DeleteAuthorCommandValidator vld = new DeleteAuthorCommandValidator();
            var result= vld.Validate(cmd);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteAuthorCommand cmd = new DeleteAuthorCommand(null);
            cmd.AuthorIdDto = 1;

            DeleteAuthorCommandValidator vld = new DeleteAuthorCommandValidator();
            var result = vld.Validate(cmd);

            result.Errors.Count.Should().Be(0);
        }
    }
}