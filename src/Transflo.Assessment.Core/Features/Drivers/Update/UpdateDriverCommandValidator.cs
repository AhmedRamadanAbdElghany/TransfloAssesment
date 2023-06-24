using FluentValidation;
using Transflo.Assessment.Core.Constans;

namespace Transflo.Assessment.Core.Features.Drivers.Update
{
    public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
    {
        private const string PhoneNoRegex = @"^(?:(?:\+|00)([1-9]\d{0,2}))?([- ])?\d{1,4}\1?\d{1,4}\1?\d{1,4}$";

        public UpdateDriverCommandValidator()
        {
            // TODO: Use Resource File for the messages 

            RuleFor(p => p.Id).GreaterThan(0).WithMessage("Id Is Required");

            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First Name Is Required")
                .MaximumLength(ValidationsConstatnts.DriverFirstNameMaxLength).WithMessage("Invalid First name");

            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last Name Is Required")
           .MaximumLength(ValidationsConstatnts.DriverLastNameMaxLength).WithMessage("Last First name");

            RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("Email Is Required")
                 .EmailAddress()
                 .WithMessage("invalid Email");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(PhoneNoRegex)
                .WithMessage("Invalid phone number format.");

        }
    }
}
