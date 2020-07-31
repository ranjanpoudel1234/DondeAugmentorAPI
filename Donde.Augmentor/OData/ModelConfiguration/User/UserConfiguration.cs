using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.User
{
    public class UserConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.User.UserViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var users = builder.EntitySet<ViewModels.V2.User.UserViewModel>(ODataConstants.UsersRoute).EntityType;
            return users;
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
