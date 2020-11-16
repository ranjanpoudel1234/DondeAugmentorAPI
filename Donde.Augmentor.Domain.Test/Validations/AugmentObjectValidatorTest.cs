using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Domain.Test.Validations
{
    [TestClass]
    public class AugmentObjectValidatorTest : BaseValidationTest
    {
        [TestMethod]
        public void Validate_WithValidModel_ReturnsSuccess()
        {
            var validator = new AugmentObjectValidator();

            var result = validator.Validate(validModel);

            AssertFluentValidationSuccess(result);
        }

        [TestMethod]
        public void Validate_WithInvalidId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.Id = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.Id), DondeErrorMessages.PROPERTY_EMPTY );
        }

        [TestMethod]
        public void Validate_WithInvalidTitle_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.Title = string.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.Title), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithInvalidDescription_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.Description = string.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.Description), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithInvalidOrganizationId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.OrganizationId = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.OrganizationId), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyLocations_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations = new List<AugmentObjectLocation>();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.AugmentObjectLocations), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyNonDeletedLocations_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations = invalidModel.AugmentObjectLocations.Where(l => l.IsDeleted).ToList();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.AugmentObjectLocations), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyLocationId_OnNonDeletedLocation_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations.First(x => !x.IsDeleted).Id = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[0].{nameof(AugmentObjectLocation.Id)}", DondeErrorMessages.PROPERTY_EMPTY);
        }


        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyLocationId_OnDeletedLocation_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations.First(x => x.IsDeleted).Id = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[1].{nameof(AugmentObjectLocation.Id)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyAugmentObjectId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations.Last().AugmentObjectId = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[1].{nameof(AugmentObjectLocation.AugmentObjectId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyLatitude_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations.First().Latitude = 0.0f;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[0].{nameof(AugmentObjectLocation.Latitude)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyLongitude_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectLocations.Last().Longitude = 0.0f;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[1].{nameof(AugmentObjectLocation.Longitude)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithStaticType_WithNotEmptyLocations_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.Type = Core.Domain.Enum.AugmentObjectTypes.Static;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectLocations)}[0]", DondeErrorMessages.MUST_BE_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyAugmentObjectMedia_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias = new List<AugmentObjectMedia>();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.AugmentObjectMedias), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithGeographicalType_WithEmptyNonDeletedMedia_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias = invalidModel.AugmentObjectMedias.Where(m => m.IsDeleted).ToList();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, nameof(AugmentObject.AugmentObjectMedias), DondeErrorMessages.PROPERTY_EMPTY);
        }


        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithEmptyId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().Id = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.Id)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithEmptyAugmentObjectId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().AugmentObjectId = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.AugmentObjectId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithEmptyAugmentObjectId_EvenOnDeletedObject_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First(m => m.IsDeleted).AugmentObjectId = Guid.Empty;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[1].{nameof(AugmentObjectMedia.AugmentObjectId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithAvatarWithAudio_WithNullAvatarId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().MediaType = AugmentObjectMediaTypes.AvatarWithAudio;
            invalidModel.AugmentObjectMedias.First().AvatarId = null; //because its nullable, empty wont suffice

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.AvatarId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithAvatarWithAudio_WithEmptyAudioId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.Last().MediaType = AugmentObjectMediaTypes.AvatarWithAudio;
            invalidModel.AugmentObjectMedias.Last().AudioId = null;

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[1].{nameof(AugmentObjectMedia.AudioId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithAvatarWithAudio_WithNotEmptyVideoId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().MediaType = AugmentObjectMediaTypes.AvatarWithAudio;
            invalidModel.AugmentObjectMedias.First().VideoId = SequentialGuidGenerator.GenerateComb();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.VideoId)}", DondeErrorMessages.MUST_BE_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithVideo_WithNullVideoId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().MediaType = AugmentObjectMediaTypes.Video;
            invalidModel.AugmentObjectMedias.First().VideoId = null; //because its nullable, empty wont suffice

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.VideoId)}", DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithVideo_WithNotEmptyAvatarId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().MediaType = AugmentObjectMediaTypes.Video;
            invalidModel.AugmentObjectMedias.First().AvatarId = SequentialGuidGenerator.GenerateComb();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.AvatarId)}", DondeErrorMessages.MUST_BE_EMPTY);
        }

        [TestMethod]
        public void Validate_WithAugmentObjectMedia_WithVideo_WithNotEmptyAudioId_ReturnsError()
        {
            var validator = new AugmentObjectValidator();

            var invalidModel = validModel;
            invalidModel.AugmentObjectMedias.First().MediaType = AugmentObjectMediaTypes.Video;
            invalidModel.AugmentObjectMedias.First().AudioId = SequentialGuidGenerator.GenerateComb();

            var result = validator.Validate(invalidModel);

            AssertFluentValidationError(result, $"{nameof(AugmentObject.AugmentObjectMedias)}[0].{nameof(AugmentObjectMedia.AudioId)}", DondeErrorMessages.MUST_BE_EMPTY);
        }

        AugmentObject validModel = new AugmentObject()
        {
            Id = SequentialGuidGenerator.GenerateComb(),
            AugmentImageId = SequentialGuidGenerator.GenerateComb(),
            Title = "Test AugmentObject",
            Description = "Description AugmentObject",
            Type = AugmentObjectTypes.Geographical,
            OrganizationId = SequentialGuidGenerator.GenerateComb(),
            AugmentObjectMedias = new List<AugmentObjectMedia>
            {
                new AugmentObjectMedia()
                {
                    Id = SequentialGuidGenerator.GenerateComb(),
                    MediaType = AugmentObjectMediaTypes.AvatarWithAudio,
                    AvatarId = SequentialGuidGenerator.GenerateComb(),
                    AudioId = SequentialGuidGenerator.GenerateComb(),
                    AugmentObjectId = SequentialGuidGenerator.GenerateComb()
                },
                  new AugmentObjectMedia()
                {
                    Id = SequentialGuidGenerator.GenerateComb(),
                    MediaType = AugmentObjectMediaTypes.AvatarWithAudio,
                    AvatarId = SequentialGuidGenerator.GenerateComb(),
                    AudioId = SequentialGuidGenerator.GenerateComb(),
                    AugmentObjectId = SequentialGuidGenerator.GenerateComb(),
                    IsDeleted = true
                }
            },
            AugmentObjectLocations = new List<AugmentObjectLocation>
            {
                new AugmentObjectLocation()
                {
                    Id = SequentialGuidGenerator.GenerateComb(),
                    AugmentObjectId = SequentialGuidGenerator.GenerateComb(),
                    Latitude = 20.56f,
                    Longitude = 30.65f
                },
                new AugmentObjectLocation()
                {
                    Id = SequentialGuidGenerator.GenerateComb(),
                    AugmentObjectId = SequentialGuidGenerator.GenerateComb(),
                    Latitude = 20.56f,
                    Longitude = 30.65f,
                    IsDeleted = true
                }
            }
        };
    }
}
