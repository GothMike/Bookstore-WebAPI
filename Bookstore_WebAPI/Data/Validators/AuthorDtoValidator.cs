using Bookstore_WebAPI.Data.Models.Dto;
using FluentValidation;

namespace Bookstore_WebAPI.Data.Validators
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);

            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15);
        }
    }
}
