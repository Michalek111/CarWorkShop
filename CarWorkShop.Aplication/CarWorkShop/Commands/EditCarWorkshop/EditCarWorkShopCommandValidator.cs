using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.EditCarWorkshop
{
    public class EditCarWorkShopCommandValidator : AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkShopCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter description");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(9)
                .MaximumLength(12);
        }
    }
}
