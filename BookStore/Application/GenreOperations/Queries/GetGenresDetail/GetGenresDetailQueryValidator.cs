﻿using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQueryValidator: AbstractValidator<GetGenresDetailQuery>
    {
        public GetGenresDetailQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }
    }
}
