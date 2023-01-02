using FluentValidation;

namespace BookStore.Application.BookOperations.Command.UpdateBooks
{
    public class UpdateBooksCommandValidator : AbstractValidator<UpdateBooksCommand>
    {
        public UpdateBooksCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }

    }
}
