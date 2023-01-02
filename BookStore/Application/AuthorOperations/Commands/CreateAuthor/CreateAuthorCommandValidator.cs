using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator: AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty();
            RuleFor(command=>command.Model.SurName).NotEmpty();
            RuleFor(command=>command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.AddYears(-18));
        }
    }
}
