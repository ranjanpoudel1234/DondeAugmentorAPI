using Donde.Augmentor.Core.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.ViewModels.AugmentObject
{
    public interface IAugmentObjectViewModel
    {
         Guid Id { get; set; }
         Guid AugmentImageId { get; set; }
         string Title { get; set; }
         string Description { get; set; }
         AugmentObjectTypes Type { get; set; }
         Guid OrganizationId { get; set; }

         DateTime AddedDate { get; set; }
         DateTime UpdatedDate { get; set; }
         bool IsActive { get; set; }
         AugmentObjectMediaTypes MediaType { get; set; }
         Guid? AvatarId { get; set; }
         string AvatarName { get; set; }
         string AvatarUrl { get; set; }
         string AvatarConfigurationString { get; set; }
         AvatarConfigurationViewModel AvatarConfiguration { get; set; }
         Guid? AudioId { get; set; }
         string AudioName { get; set; }
         string AudioUrl { get; set; }
         Guid? VideoId { get; set; }
         string VideoName { get; set; }
         string VideoUrl { get; set; }

         string ImageName { get; set; }
         string ImageUrl { get; set; }

         List<AugmentObjectMediaViewModel> AugmentObjectMedias { get; set; }

         List<AugmentObjectLocationViewModel> AugmentObjectLocations { get; set; }
    }
}
