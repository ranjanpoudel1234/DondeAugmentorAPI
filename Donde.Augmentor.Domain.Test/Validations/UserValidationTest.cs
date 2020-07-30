using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Domain.Validations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Donde.Augmentor.Core.Domain.DomainConstants;

namespace Donde.Augmentor.Domain.Test.Validations
{
    [TestClass]
    public class UserValidationTest : BaseValidationTest
    {
        [TestMethod]
        public void Validate_WithValidModel_ReturnsSuccess()
        {
            var validator = new UserValidator();

            var result = validator.Validate(validModel);

            AssertFluentValidationSuccess(result);
        }

        [TestMethod]
        public void Validate_WithEmptyId_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.Id = Guid.Empty;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.Id), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithEmptyUserName_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.UserName = string.Empty;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.UserName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullUserName_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.UserName = null;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.UserName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullFirstName_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.FirstName = null;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.FirstName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullLastName_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.LastName = null;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.LastName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullFullName_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.FullName = null;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.FullName), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithNullEmail_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.Email = null;

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.Email), DondeErrorMessages.PROPERTY_EMPTY);
        }

        [TestMethod]
        public void Validate_WithInvalidEmail_ReturnsError()
        {
            var invalidUser = validModel;
            invalidUser.Email = "Test.tt";

            var validator = new UserValidator();

            var result = validator.Validate(invalidUser);

            AssertFluentValidationError(result, nameof(User.Email), DondeErrorMessages.INVALID_VALUE);
        }


        User validModel = new User()
        {
            Id = SequentialGuidGenerator.GenerateComb(),
            UserName = "TestUserName",
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            FullName = "TestFullName",
            Email = "TestEmail@gmail.com"
        };
    }
}
