using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopRating.Commands
{
    public class CreateCarWorkshopRatingValidator : AbstractValidator<CreateCarWorkshopRatingCommand>
    {
        public CreateCarWorkshopRatingValidator()
        {
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("This field is required")
                .MinimumLength(2).WithMessage("Description should have minimum 2 characters");
            RuleFor(r => r.Rate)
                .NotEmpty()
                .GreaterThanOrEqualTo(1).WithMessage("Minimum rate is 1")
                .LessThanOrEqualTo(5).WithMessage("Maximum rate is 5");
        }
    }
}
