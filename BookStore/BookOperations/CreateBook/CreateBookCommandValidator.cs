using FluentValidation;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBooksCommand>
    {
        public CreateBookCommandValidator() 
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);

        }
    }
}
