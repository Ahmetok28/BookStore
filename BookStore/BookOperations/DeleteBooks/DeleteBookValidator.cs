using BookStore.BookOperations.CreateBook;
using FluentValidation;

namespace BookStore.BookOperations.DeleteBooks
{
    public class DeleteBookValidator: AbstractValidator<DeleteBooks>
    {
        public DeleteBookValidator()
        {
            RuleFor(command=>command._id).GreaterThan(0); 
        }
    }
}
