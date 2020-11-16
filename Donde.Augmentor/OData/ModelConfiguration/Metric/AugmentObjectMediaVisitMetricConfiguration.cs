using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1.Metric;
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
            var augmentObjectMediaVisit = builder.EntitySet<AugmentObjectMediaVisitMetricViewModel>(ODataConstants.AugmentObjectMediaVisitMetricRoute).EntityType;
            return augmentObjectMediaVisit;
        }

        private EntityTypeConfiguration<ViewModels.V2.Metric.TargetMediaVisitMetricViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var targetMediaVisit = builder.EntitySet<ViewModels.V2.Metric.TargetMediaVisitMetricViewModel>(ODataConstants.TargetMediaVisitMetricRoute).EntityType;
            return targetMediaVisit;
        }

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            switch (apiVersion.MajorVersion)
            {
                case 1:
                    ConfigureV1(builder);
                    break;
                case 2:
                    ConfigureV2(builder);
                    break;
                default:
                    ConfigureCurrent(builder);
                    break;
            }
        }
    }
}
