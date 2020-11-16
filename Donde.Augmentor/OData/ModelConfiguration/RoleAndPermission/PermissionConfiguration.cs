using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.RoleAndPermission
{
    public class PermissionConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.RolesAndPermission.PermissionViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var entity = builder.EntitySet<ViewModels.V2.RolesAndPermission.PermissionViewModel>(ODataConstants.PermissionsRoute).EntityType;
            return entity;
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
