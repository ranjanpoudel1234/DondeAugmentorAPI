using Donde.Augmentor.Core.Domain.Models.Metrics;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.Metric
{
    public class AugmentObjectMediaVisitMetricConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            var release = ConfigureCurrent(builder);
        }

        private EntityTypeConfiguration<AugmentObjectMediaVisitMetricViewModel> ConfigureCurrent(ODataModelBuilder builder)
        {
            var audios = builder.EntitySet<AugmentObjectMediaVisitMetricViewModel>("augmentObjectMediaVisitMetrics").EntityType;
            return audios;
        }

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            switch (apiVersion.MajorVersion)
            {
                case 1:
                    ConfigureV1(builder);
                    break;
                default:
                    ConfigureCurrent(builder);
                    break;
            }
        }
    }
}
