using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.UpdateGener
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).MinimumLength(4).When(x=>x.Model.Name.Trim() != string.Empty);
        }
    }
}
