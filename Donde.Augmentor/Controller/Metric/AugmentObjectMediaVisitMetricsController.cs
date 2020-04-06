using Donde.Augmentor.Core.Domain.Models.Metrics;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.Metric
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("augmentObjectMediaVisitMetrics")]
    [Authorize]
    public class AugmentObjectMediaVisitMetricsController
    {
        private readonly IAugmentObject
        public AugmentObjectMediaVisitMetricsController()
        {

        }

        [ODataRoute]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AugmentObjectMediaVisitMetricViewModel augmentObjectMediaVisitMetricViewModel)
        {
            AuthorizeByHeader();

          // authorize with some const header value.
        }
    }
}
