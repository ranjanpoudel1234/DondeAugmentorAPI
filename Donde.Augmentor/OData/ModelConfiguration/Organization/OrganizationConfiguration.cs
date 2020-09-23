using Donde.Augmentor.Web.ViewModels.V1;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration
{
    public class OrganizationConfiguration : IModelConfiguration
    {  
        private EntityTypeConfiguration<OrganizationViewModel> ConfigureV1(ODataModelBuilder builder)
        {
            var organizations = builder.EntitySet<OrganizationViewModel>(ODataConstants.OrganizationRoute).EntityType;
            return organizations;
        }

        private EntityTypeConfiguration<ViewModels.V2.Organization.OrganizationViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var organizations = builder.EntitySet<ViewModels.V2.Organization.OrganizationViewModel>(ODataConstants.OrganizationRoute).EntityType;
            return organizations;
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
                    ConfigureV1(builder);
                    break;
            }
        }
    }
}
