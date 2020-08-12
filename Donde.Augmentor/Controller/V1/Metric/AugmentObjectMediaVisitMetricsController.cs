using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using Donde.Augmentor.Web.ViewModels.V1;
using Donde.Augmentor.Web.ViewModels.V1.Metric;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V1.Metric
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentObjectMediaVisitMetrics")]
    [Authorize]
    public class AugmentObjectMediaVisitMetricsController : BaseController
    {
        private readonly IAugmentObjectMediaVisitMetricService _augmentObjectMediaVisitMetricService;
        private readonly IMapper _mapper;

        public AugmentObjectMediaVisitMetricsController(
            IAugmentObjectMediaVisitMetricService augmentObjectMediaVisitMetricService,
            IMapper mapper)
        {
            _augmentObjectMediaVisitMetricService = augmentObjectMediaVisitMetricService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AugmentObjectMediaVisitMetricViewModel augmentObjectMediaVisitMetricViewModel)
        {
            AuthorizeTemporariLyByHeaderOrThrow();

            var mediaVisitModel = _mapper.Map<AugmentObjectMediaVisitMetric>(augmentObjectMediaVisitMetricViewModel);

            var result = await _augmentObjectMediaVisitMetricService.CreateAugmentObjectMediaVisitMetricAsync(mediaVisitModel);

            return Created(_mapper.Map<AugmentObjectMediaVisitMetricViewModel>(result));       
        }
    }
}
