using Bookstore_WebAPI.Data.Models.Dto;
using FluentValidation;

namespace Bookstore_WebAPI.Data.Validators
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(30);
        }
    }
}
