using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.Metric;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [ODataRoutePrefix(ODataConstants.TargetMediaVisitMetricRoute)]
    [Authorize]
    public class TargetMediaVisitMetricsController : BaseController
    {
        private readonly IAugmentObjectMediaVisitMetricService _augmentObjectMediaVisitMetricService;
        private readonly IMapper _mapper;

        public TargetMediaVisitMetricsController(
            IAugmentObjectMediaVisitMetricService augmentObjectMediaVisitMetricService,
            IMapper mapper)
        {
            _augmentObjectMediaVisitMetricService = augmentObjectMediaVisitMetricService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] TargetMediaVisitMetricViewModel targetMediaVisitMetricViewModel)
        {
            AuthorizeTemporariLyByHeaderOrThrow();

            var mediaVisitModel = _mapper.Map<AugmentObjectMediaVisitMetric>(targetMediaVisitMetricViewModel);

            var result = await _augmentObjectMediaVisitMetricService.CreateAugmentObjectMediaVisitMetricAsync(mediaVisitModel, targetMediaVisitMetricViewModel.MediaId);

            var targetMediaVisitMetricViewModelToReturn = _mapper.Map<TargetMediaVisitMetricViewModel>(result);
            targetMediaVisitMetricViewModelToReturn.MediaId = targetMediaVisitMetricViewModel.MediaId;

            return Created(targetMediaVisitMetricViewModelToReturn);
        }
    }
}
