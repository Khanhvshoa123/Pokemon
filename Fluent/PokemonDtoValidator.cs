using FluentValidation;
using PokemonReviewApp.Models;
using PokemonReviewApp.NewFolder;

namespace PokemonReviewApp.Fluent
{
    public class PokemonDtoValidator : AbstractValidator<PokemonDto>
    {
        public PokemonDtoValidator() {
            RuleFor(p => p.Name)
                 .Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!")
                 .Length(2, 25);

        }
    }
}
