using Donde.Augmentor.Core.Domain.Models;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.CustomValidations
{
    public interface IAugmentObjectResourceValidationService
    {
        Task ValidateAugmentObjectResourceOrThrowAsync(AugmentObject entity);
    }
}
