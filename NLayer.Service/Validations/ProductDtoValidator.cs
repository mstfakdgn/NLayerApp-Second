using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.CategoryID).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater than 0");

            //RuleFor(x => x.Postcode).Must(BeAValidPostcode).WithMessage("Please specify a valid postcode");

            //public class AddressValidator : AbstractValidator<Address>
            //{
            //    public AddressValidator()
            //    {
            //        RuleFor(address => address.Postcode).NotNull();
            //        //etc
            //    }
            //}
            //RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
    }
    }
}
