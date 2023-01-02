
using FluentValidation;

namespace BookStore.Application.BookOperations.Command.DeleteBooks
{
    public class DeleteBookValidator : AbstractValidator<DeleteBooks>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
