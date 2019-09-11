using Donde.Augmentor.Core.Domain.Dto;
using FluentValidation;
using System.IO;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public class MediaAttachmentDtoValidator : AbstractValidator<MediaAttachmentDto>
    {
        public const string DefaultRuleSet = "default";
        public const string ImageFileRuleSet = "ImageFileRuleSet";
        public const string VideoFileRuleSet = "VideoFileRuleSet";

        public MediaAttachmentDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.FilePath).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.FileName).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);
            RuleFor(x => x.MimeType).NotEmpty().WithMessage(DondeErrorMessages.PROPERTY_EMPTY);

            RuleSet(ImageFileRuleSet, () => {
                RuleFor(x => x.FilePath).Must(BeValidImageFile).WithMessage(DondeErrorMessages.INVALID_IMAGE_FILE_TYPE);
            });

            RuleSet(VideoFileRuleSet, () => {
                RuleFor(x => x.FilePath).Must(BeValidVideoFile).WithMessage(DondeErrorMessages.INVALID_VIDEO_FILE_TYPE);
            });
      
            bool BeValidImageFile(string filePath)
            {
                var fileExtension = Path.GetExtension(filePath);
                return DomainConstants.ValidMediaExtensions.VALID_IMAGE_EXTENSIONS.Contains(fileExtension.ToLower());
            }

            bool BeValidVideoFile(string filePath)
            {
                var fileExtension = Path.GetExtension(filePath);
                return DomainConstants.ValidMediaExtensions.VALID_VIDEO_EXTENSIONS.Contains(fileExtension.ToLower());
            }
        }
    }
}
