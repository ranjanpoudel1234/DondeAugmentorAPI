using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    public class AugmentObjectController
    {
        private readonly IAugmentObjectService _augmentObjectService;
        
        public AugmentObjectController(IAugmentObjectService augmentObjectService)
        {
            _augmentObjectService = augmentObjectService;
        }

        //public async Task<IActionResult> GetAugmentObject(double latitude, double longitude)
        //{
             
        //}
    }
}
