using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.Organization
{
    public class SiteConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.Organization.SiteViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var sites = builder.EntitySet<ViewModels.V2.Organization.SiteViewModel>(ODataConstants.SitesRoute).EntityType;
            return sites;
        }

        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            switch (apiVersion.MajorVersion)
            {
                case 2:
                    ConfigureV2(builder);
                    break;
                default:
                    ConfigureV2(builder);
                    break;
            }
        }
    }
}
