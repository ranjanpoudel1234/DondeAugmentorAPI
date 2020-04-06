using AutoMapper;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces.Metric;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.Metric
{
    public class AugmentObjectVisitMetricsController : BaseController
    {
        private readonly IAugmentObjectVisitMetricService _augmentObjectVisitMetricService;
        private readonly IMapper _mapper;

        public AugmentObjectVisitMetricsController(
            IAugmentObjectVisitMetricService augmentObjectVisitMetricService,
            IMapper mapper)
        {
            _augmentObjectVisitMetricService = augmentObjectVisitMetricService;
            _mapper = mapper;
        }

        [ODataRoute]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AugmentObjectVisitMetricViewModel augmentObjectVisitMetricViewModel)
        {
            AuthorizeTemporariLyByHeaderOrThrow();

            var mediaVisitModel = _mapper.Map<AugmentObjectVisitMetric>(augmentObjectVisitMetricViewModel);

            var result = await _augmentObjectVisitMetricService.CreateAugmentObjectVisitMetricAsync(mediaVisitModel);

            return Created(_mapper.Map<AugmentObjectMediaVisitMetricViewModel>(result));

        }
    }
}
