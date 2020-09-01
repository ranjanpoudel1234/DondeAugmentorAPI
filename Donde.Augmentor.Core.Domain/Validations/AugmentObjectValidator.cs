using Donde.Augmentor.Core.Domain.Models;
using FluentValidation;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public class AugmentObjectValidator : AbstractValidator<AugmentObject>
    {
        public AugmentObjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.AugmentImageId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Title).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Description).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.OrganizationId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);

            When(x => x.Type == Enum.AugmentObjectTypes.Geographical,
                () => RuleFor(x => x.AugmentObjectLocations).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY));
            When(x => x.Type == Enum.AugmentObjectTypes.Geographical,
              () => RuleForEach(x => x.AugmentObjectLocations).SetValidator(new AugmentObjectLocationValidator()));
            When(x => x.Type == Enum.AugmentObjectTypes.Static,
                () => RuleFor(x => x.AugmentObjectLocations).Empty().WithMessage(DondeErrorMessages.MUST_BE_EMPTY));

            RuleFor(x => x.AugmentObjectMedias).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleForEach(x => x.AugmentObjectMedias).SetValidator(new AugmentObjectMediaValidator());
        }
    }

    public class AugmentObjectLocationValidator : AbstractValidator<AugmentObjectLocation>
    {
        public AugmentObjectLocationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.AugmentObjectId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Latitude).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.Longitude).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
        }
    }

    public class AugmentObjectMediaValidator : AbstractValidator<AugmentObjectMedia>
    {
        public AugmentObjectMediaValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.AugmentObjectId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            When(x => x.MediaType == Enum.AugmentObjectMediaTypes.AvatarWithAudio,
                () =>
                {
                    RuleFor(x => x.AvatarId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                    RuleFor(x => x.AudioId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
                    RuleFor(x => x.VideoId).Empty().WithMessage(DondeErrorMessages.MUST_BE_EMPTY);
                }
                );
            When(x => x.MediaType == Enum.AugmentObjectMediaTypes.Video,
               () =>
               {
                   RuleFor(x => x.AvatarId).Empty().WithMessage(DondeErrorMessages.MUST_BE_EMPTY);
                   RuleFor(x => x.AudioId).Empty().WithMessage(DondeErrorMessages.MUST_BE_EMPTY);
                   RuleFor(x => x.VideoId).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
               }
               );
        }
    }
}
