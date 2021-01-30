using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Donde.Augmentor.Web.OData.ModelConfiguration.User
{
    public class UserInfoConfiguration : IModelConfiguration
    {
        private EntityTypeConfiguration<ViewModels.V2.User.UserInfoViewModel> ConfigureV2(ODataModelBuilder builder)
        {
            var users = builder.EntitySet<ViewModels.V2.User.UserInfoViewModel>(ODataConstants.UserInfo).EntityType;
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
