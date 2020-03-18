using Donde.Augmentor.Core.Domain.CustomExceptions;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public static class ValidationExtensionMethods
    {
        public static async Task ValidateOrThrowAsync<T>(this IValidator<T> validator, T validatee, string ruleSets = "*")
        {
           var result = await validator.ValidateAsync(validatee, ruleSet: ruleSets);

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
                var errorMessageConcatenated = string.Join(";", errorMessage.Select(x => x.Key + "|" + x.Value).ToArray());
                throw new HttpBadRequestException(errorMessageConcatenated);
            }
        }
    }
}
