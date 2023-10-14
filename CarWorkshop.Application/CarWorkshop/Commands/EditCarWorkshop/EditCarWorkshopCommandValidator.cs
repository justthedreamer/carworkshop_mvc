using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshop;
using CarWorkshop.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshop
{
    public class EditCarWorkshopCommandValidator : AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkshopCommandValidator(ICarWorkshopRepository repository) 
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("This field is required");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8).WithMessage("PhoneNumber should have minimum 2 characters")
                .MaximumLength(12).WithMessage("PhoneNumber should have maximum 20 characters");
        }
    }
}
