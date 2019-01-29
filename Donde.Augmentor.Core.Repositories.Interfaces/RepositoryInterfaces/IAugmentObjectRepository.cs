using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces
{
    public interface IAugmentObjectRepository
    {
        Task<AugmentObject> GetAugmentObjectByImageId(Guid imageId);//just an example
    }
}
