using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
       public DeleteAuthorCommandValidator() 
        {
            RuleFor(command => command.AuthorIdDto).GreaterThan(0);
           // RuleFor(command => command.IsPublishing).NotEqual(true);
        }
    }
}
