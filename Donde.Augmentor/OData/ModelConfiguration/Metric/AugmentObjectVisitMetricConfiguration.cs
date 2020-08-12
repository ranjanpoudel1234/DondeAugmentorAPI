using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1.Metric;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.Metric
{
    public class AugmentObjectVisitMetricConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            var release = ConfigureCurrent(builder);
        }

        private EntityTypeConfiguration<AugmentObjectVisitMetricViewModel> ConfigureCurrent(ODataModelBuilder builder)
        {
            var audios = builder.EntitySet<AugmentObjectVisitMetricViewModel>("augmentObjectVisitMetrics").EntityType;
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

