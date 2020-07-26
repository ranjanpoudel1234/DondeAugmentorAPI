using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Donde.Augmentor.Core.Domain.DomainConstants;
using FluentValidation;

namespace Donde.Augmentor.Domain.Test.Validations
{
    [TestClass]
    public class OrganizationValidatorTest : BaseValidationTest
    {
        [TestMethod]
        public void Validate_WithValidModel_ReturnsSuccess()
        {
            var validator = new OrganizationValidator();

            var result = validator.Validate(validOrganizationModel);

            AssertFluentValidationSuccess(result);
        }

        [TestMethod]
        public void Validate_WithEmptyId_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.Id = Guid.Empty;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.Id), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyName_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.Name = string.Empty;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.Name), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullName_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.Name = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.Name), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyStreetAddress1_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.StreetAddress1 = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.StreetAddress1), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyCity_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.City = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.City), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyState_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.State = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.State), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyZip_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.Zip = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.Zip), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithInvalidZip_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.Zip = "704255";

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization);

            AssertFluentValidationError(result, nameof(Organization.Zip), DondeErrorMessages.INVALID_VALUE);
        }

        [TestMethod]
        public void Validate_WithOrganizationUpdateRuleSet_WithInvalidLogoName_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.LogoName = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization, ruleSet: $"{OrganizationValidator.OrganizationUpdateRuleSet}");

            AssertFluentValidationError(result, nameof(Organization.LogoName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithOrganizationUpdateRuleSet_WithInvalidLogoUrl_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.LogoUrl = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization, ruleSet: $"{OrganizationValidator.OrganizationUpdateRuleSet}");

            AssertFluentValidationError(result, nameof(Organization.LogoUrl), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithOrganizationUpdateRuleSet_WithInvalidLogoMimeType_ReturnsError()
        {
            var invalidOrganization = validOrganizationModel;
            invalidOrganization.LogoMimeType = null;

            var validator = new OrganizationValidator();

            var result = validator.Validate(invalidOrganization, ruleSet: $"{OrganizationValidator.OrganizationUpdateRuleSet}");

            AssertFluentValidationError(result, nameof(Organization.LogoMimeType), DondeErrorMessages.PROPERTY_EMPTY);
        }

        Organization validOrganizationModel = new Organization()
        {
            Id = SequentialGuidGenerator.GenerateComb(),
            Name = "TestOrg",
            ShortName = "SELU",
            StreetAddress1 = "TestStreetAddress1",
            StreetAddress2 = null,
            City = "TestCity",
            State = "TestState",
            Zip = "70430",
            LogoName = "Test.png",
            LogoMimeType = "image/png",
            Type = Core.Domain.Enum.OrganizationTypes.University
        };
    }
}
