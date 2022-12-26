using FluentValidation;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBookByIdValidator:AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
