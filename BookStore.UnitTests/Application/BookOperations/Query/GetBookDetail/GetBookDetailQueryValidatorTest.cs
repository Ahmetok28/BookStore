using BookStore.Application.BookOperations.Queries.GetBookDetail;
using FluentAssertions;


namespace BookStore.UnitTests.Application.BookOperations.Query.GetBookDetail
{

    public class GetBookDetailQueryValidatorTest
    {
        [Fact]
        public void WhenInvalidIdIsGıven_Validator_ShouldBeReturnError()
        {
            //Arrange
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId= 0;

            //Act
            GetBookByIdValidator vld = new GetBookByIdValidator();
            var result=  vld.Validate(query);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            //Arrange
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = 1;

            //Act
            GetBookByIdValidator vld = new GetBookByIdValidator();
            var result = vld.Validate(query);

            //Assert
            result.Errors.Count.Should().Be(0);
        }

    }
}
