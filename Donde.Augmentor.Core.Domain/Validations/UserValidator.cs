using Donde.Augmentor.Core.Domain.Models.Identity;
using FluentValidation;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.FullName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            When(x => x.Email != null, () => RuleFor(x => x.Email).EmailAddress().WithMessage(DondeErrorMessages.INVALID_VALUE));
        }
    }
}
