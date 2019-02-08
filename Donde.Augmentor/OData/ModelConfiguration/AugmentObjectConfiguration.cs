using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.OData.ModelConfiguration
{
    public class AugmentObjectConfiguration : IModelConfiguration
    {
        private void ConfigureV1(ODataModelBuilder builder)
        {
            var release = ConfigureCurrent(builder);
        }

        private EntityTypeConfiguration<AugmentObjectViewModel> ConfigureCurrent(ODataModelBuilder builder)
        {
            var augmentObject = builder.EntitySet<AugmentObjectViewModel>("augmentObjects").EntityType;
            return augmentObject;
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
