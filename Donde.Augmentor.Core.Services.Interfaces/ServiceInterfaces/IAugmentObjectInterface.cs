using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.Models;

namespace Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces
{
    public interface IAugmentObjectInterface
    {
        Task<AugmentObject> GetAugmentObjectsWithinRadiusAsync(int radius);
    }
}
