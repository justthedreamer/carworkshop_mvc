using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkshopCommandValidator(ICarWorkshopRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("This field is required")
                .MinimumLength(2).WithMessage("Name should have minimum 2 characters")
                .MaximumLength(20).WithMessage("Name should have maximum 20 characters")
                .Custom((value, context) =>
                {
                    var existringCarWorkshop = repository.GetByName(value).Result;
                    if (existringCarWorkshop != null)
                    {
                        context.AddFailure($"This name : \"{value}\" arleady existing.");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("This field is required");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8).WithMessage("PhoneNumber should have minimum 2 characters")
                .MaximumLength(12).WithMessage("PhoneNumber should have maximum 20 characters");

        }
    }
}
