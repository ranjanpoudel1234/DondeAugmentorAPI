using Donde.Augmentor.Web.ViewModels.V1.AugmentObject;
using Donde.Augmentor.Web.ViewModels.V2.Targets;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration
{
    public class TargetConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            var release = ConfigureCurrent(builder);
        }

        private EntityTypeConfiguration<AugmentObjectViewModel> ConfigureCurrent(ODataModelBuilder builder)
        {
            var augmentObject = builder.EntitySet<AugmentObjectViewModel>(ODataConstants.AugmentObjectsRoute).EntityType;
            return augmentObject;
        }

        private EntityTypeConfiguration<TargetViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var target = builder.EntitySet<TargetViewModel>(ODataConstants.TargetsRoute).EntityType;
            return target;
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
