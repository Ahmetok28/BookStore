using BookStore.Application.AuthorOperations.Query.GetAuthorsDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Query.GetAuthorDeatail
{
    public class GetAuthorDetailValidatorTest
    {
        [Fact]
        public void WhenInvalidIdIsGıven_Validator_ShouldBeReturnError()
        {
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(null, null);
            query.AuthorId = 0;

            GetAuthorsDetailQueryValidator vld = new GetAuthorsDetailQueryValidator();
            var result = vld.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(null, null);
            query.AuthorId = 1;

            GetAuthorsDetailQueryValidator vld = new GetAuthorsDetailQueryValidator();
            var result = vld.Validate(query);

            result.Errors.Count.Should().Be(0);

        }
    }
}
