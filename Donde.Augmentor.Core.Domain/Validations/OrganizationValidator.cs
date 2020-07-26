using Donde.Augmentor.Core.Domain.Models;
using FluentValidation;
using System.Text.RegularExpressions;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public class OrganizationValidator : AbstractValidator<Organization>
    {
        public const string DefaultRuleSet = "default";
        public const string OrganizationUpdateRuleSet = "OrganizationUpdateRuleSet";
        public OrganizationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Name).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.StreetAddress1).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.City).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.State).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Zip)
                .NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);

            When(x => x.Zip != null, () => RuleFor(x => x.Zip).Must(IsUSOrCanadianZipCode).WithMessage(DondeErrorMessages.INVALID_VALUE));
                                
            RuleSet(OrganizationUpdateRuleSet, () =>
            {
                RuleFor(x => x.LogoName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                RuleFor(x => x.LogoUrl).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                RuleFor(x => x.LogoMimeType).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            });       
        }

        private bool IsUSOrCanadianZipCode(string zipCode)
        {
            var _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            var validZipCode = true;
            if ((!Regex.Match(zipCode, _usZipRegEx).Success) && (!Regex.Match(zipCode, _caZipRegEx).Success))
            {
                validZipCode = false;
            }
            return validZipCode;
        }
    }
}
