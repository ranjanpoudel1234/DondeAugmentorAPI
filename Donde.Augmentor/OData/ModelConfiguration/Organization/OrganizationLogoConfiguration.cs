using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.Organization
{
    public class OrganizationLogoConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.Organization.OrganizationLogoViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var organizationLogos = builder.EntitySet<ViewModels.V2.Organization.OrganizationLogoViewModel>(ODataConstants.OrganizationLogoRoute).EntityType;
            return organizationLogos;
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
