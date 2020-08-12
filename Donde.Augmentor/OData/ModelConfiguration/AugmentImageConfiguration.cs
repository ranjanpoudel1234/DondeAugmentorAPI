using Donde.Augmentor.Web.ViewModels;
using Donde.Augmentor.Web.ViewModels.V1;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration
{
    public class AugmentImageConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            var release = ConfigureCurrent(builder);
        }

        private EntityTypeConfiguration<AugmentImageViewModel> ConfigureCurrent(ODataModelBuilder builder)
        {
            var augmentImages = builder.EntitySet<AugmentImageViewModel>("augmentImages").EntityType;
            return augmentImages;
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
