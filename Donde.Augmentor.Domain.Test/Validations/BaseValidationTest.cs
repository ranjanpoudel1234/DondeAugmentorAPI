using FluentValidation.Results;
using Shouldly;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Donde.Augmentor.Domain.Test.Validations
{
    public class BaseValidationTest
    {
        public void AssertFluentValidationError(ValidationResult result, string propertyInCheck, string expectedErrorMessage)
        {
            result.ShouldNotBeNull();
            result.Errors.ShouldNotBeNull();
            result.IsValid.ShouldBeFalse();
            result.Errors.Any(e => e.PropertyName.Equals(propertyInCheck, StringComparison.InvariantCultureIgnoreCase)
                                   && e.ErrorMessage.Equals(expectedErrorMessage, StringComparison.InvariantCultureIgnoreCase))
                .ShouldBeTrue();
        }

        public void AssertFluentValidationSuccess(ValidationResult result)
        {
            result.ShouldNotBeNull();
            result.Errors.ShouldBeEmpty();
            result.IsValid.ShouldBeTrue();
        }

        public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var lamdaBody = propertyLambda.Body as MemberExpression;

            if (lamdaBody == null) throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");

            return lamdaBody.Member.Name;
        }
    }
}
