using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.RoleAndPermission
{
    public class RoleConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.RolesAndPermission.RoleViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var roles = builder.EntitySet<ViewModels.V2.RolesAndPermission.RoleViewModel>(ODataConstants.RolesRoute).EntityType;
            return roles;
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
