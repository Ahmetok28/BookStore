using System.Data;
using FluentValidation;

namespace BookStore.Application.AuthorOperations.Query.GetAuthorsDetail
{
    public class GetAuthorsDetailQueryValidator:AbstractValidator<GetAuthorsDetailQuery>
    {
        public GetAuthorsDetailQueryValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}
