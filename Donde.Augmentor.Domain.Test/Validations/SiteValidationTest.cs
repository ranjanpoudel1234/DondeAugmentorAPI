using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Domain.Test.Validations
{
    [TestClass]
    public class SiteValidationTest : BaseValidationTest
    {
        [TestMethod]
        public void Validate_WithValidModel_ReturnsSuccess()
        {
            var validator = new SiteValidator();

            var result = validator.Validate(validModel);

            AssertFluentValidationSuccess(result);
        }

        [TestMethod]
        public void Validate_WithEmptyId_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Id = Guid.Empty;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Id), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyName_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Name = string.Empty;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Name), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullName_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Name = null;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Name), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyStreetAddress1_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.StreetAddress1 = null;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.StreetAddress1), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyCity_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.City = null;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.City), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyState_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.State = null;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.State), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyZip_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Zip = null;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Zip), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithInvalidZip_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Zip = "704255";

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Zip), DondeErrorMessages.INVALID_VALUE);
        }

        [TestMethod]
        public void Validate_WithEmptyLatitude_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Latitude = 0;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Latitude), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyLongitude_ReturnsError()
        {
            var invalidOrganization = validModel;
            invalidOrganization.Longitude = 0;

            var validator = new SiteValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Site.Longitude), DondeErrorMessages.PROPERTY_EMPTY);
        }

        Site validModel = new Site()
        {
            Id = SequentialGuidGenerator.GenerateComb(),
            OrganizationId = SequentialGuidGenerator.GenerateComb(),
            Name = "TestOrg",
            ShortName = null,
            StreetAddress1 = "TestStreetAddress1",
            StreetAddress2 = null,
            City = "TestCity",
            State = "TestState",
            Zip = "70430",
            Latitude = 30.63f,
            Longitude = -30.69f,
            Type = Core.Domain.Enum.SiteTypes.Main
        };
    }
}
