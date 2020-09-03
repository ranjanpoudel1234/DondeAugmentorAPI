using Donde.Augmentor.Core.Domain.Models;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Services.Services.CustomValidations
{
    public interface IAugmentObjectResourceValidator
    {
        Task ValidateAugmentObjectResourceOrThrowAsync(AugmentObject entity);
    }
}
