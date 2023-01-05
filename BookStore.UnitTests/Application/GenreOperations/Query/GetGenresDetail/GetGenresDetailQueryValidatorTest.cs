using BookStore.Application.GenreOperations.Queries.GetGenresDetail;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Query.GetGenresDetail
{
    public class GetGenresDetailQueryValidatorTest
    {
        [Fact]
        public void WhenInvalidIdIsGıven_Validator_ShouldBeReturnError()
        {
            GetGenresDetailQuery query = new GetGenresDetailQuery(null, null);
            query.GenreId = 0;

            GetGenresDetailQueryValidator vld = new GetGenresDetailQueryValidator();
            var result = vld.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            GetGenresDetailQuery query = new GetGenresDetailQuery(null, null);
            query.GenreId = 1;

            GetGenresDetailQueryValidator vld = new GetGenresDetailQueryValidator();
            var result = vld.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}
