using Donde.Augmentor.Core.Domain.CustomExceptions;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Domain.Validations
{
    public static class ValidationExtensionMethods
    {
        /// <summary>
        /// Format is Property1|Error1;Error2||Property2|Error1;Error2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <param name="validatee"></param>
        /// <param name="ruleSets"></param>
        /// <returns></returns>
        public static async Task ValidateOrThrowAsync<T>(this IValidator<T> validator, T validatee, string ruleSets = "*")
        {
           var result = await validator.ValidateAsync(validatee, ruleSet: ruleSets);
       
            if (!result.IsValid)
            {
                var resultDictionary = new Dictionary<string, List<string>>();
                foreach (var error in result.Errors)
                {
                    if (!resultDictionary.ContainsKey(error.PropertyName))
                    {
                        resultDictionary.Add(error.PropertyName, new List<string> { error.ErrorMessage });
                    }
                    else
                    {
                        resultDictionary[error.PropertyName].Add(error.ErrorMessage);
                    }
                   
                }
             
                var errorMessageConcatenated = 
                    string.Join("||", 
                        resultDictionary.Select(x => x.Key + "|" + 
                            string.Join(";", x.Value.Select(y => y))));
                throw new HttpBadRequestException(errorMessageConcatenated);
            }
        }
    }
}
