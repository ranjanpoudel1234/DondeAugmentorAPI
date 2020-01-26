using AutoMapper;
using Donde.Augmentor.Core.Domain.Dto;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.Controller;
using Donde.Augmentor.Web.ViewModels;
using FakeItEasy;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Test.Controllers
{
    [TestClass]
    public class AugmentObjectsControllerTest
    {
        [TestMethod]
        public async Task GetAugmentObject_ReturnsOKResult()
        {
            var augmentObjectController = GetDefaultAugmentObjectController();

            var result = await augmentObjectController.GetAugmentObjectGeocoded(A.Dummy<Guid>(), A.Dummy<double>(), A.Dummy<double>(), A.Dummy<int>());

            var okResult = result as OkObjectResult;

            result.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            
        }

        private AugmentObjectsController GetDefaultAugmentObjectController(IAugmentObjectService augmentObjectService = null)
        {
            return new AugmentObjectsController(augmentObjectService ?? GetDefaultAugmentObjectService(), A.Fake<IMapper>());
        }

        private IAugmentObjectService GetDefaultAugmentObjectService()
        {
            var fakeService = A.Fake<IAugmentObjectService>();
            A.CallTo(() => fakeService.GetGeographicalAugmentObjectsByRadius(A<Guid>._, A<double>._, A<double>._, A<int>._))
                .Returns(Task.FromResult(A.CollectionOfDummy<GeographicalAugmentObjectDto>(10).AsEnumerable()));

            return fakeService;
        }
    }
}
