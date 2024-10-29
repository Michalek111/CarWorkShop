using CarWorkShop.Domain.Inferfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkShop.Application.CarWorkShop.Commands.CreateCarWorkshop
{
    public class CreateCarWorkShopCommandValidation : AbstractValidator<CreateCarWorkshopCommand>
    {
        public CreateCarWorkShopCommandValidation(ICarWorkShopRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Name should have atleast 2 characters")
                .MaximumLength(20).WithMessage("Name should have maxium 20 characters")
                .Custom((value, context) =>
                {
                    var existingCarWorkShop = repository.GetByName(value).Result;

                    if (existingCarWorkShop != null)
                    {
                        context.AddFailure($"{value} is not unique name for car workshop");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter description");

            RuleFor(c => c.PhoneNumber)
                .MinimumLength(9)
                .MaximumLength(12);
        }

    }
}
