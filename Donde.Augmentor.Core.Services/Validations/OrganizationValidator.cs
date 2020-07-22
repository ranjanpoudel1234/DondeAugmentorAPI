using Donde.Augmentor.Core.Domain.Models;
using FluentValidation;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Services.Validations
{
    public class OrganizationValidator : AbstractValidator<Organization>
    {
        public const string DefaultRuleSet = "default";
        public const string OrganizationUpdateRuleSet = "OrganizationUpdateRuleSet";
        public OrganizationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.LogoName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.LogoUrl).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.LogoMimeType).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);

            RuleSet(OrganizationUpdateRuleSet, () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.Latitude).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.Longitude).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.StreetAddress1).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.City).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.State).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                //RuleFor(x => x.Zip).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            });
        }
    }
}
