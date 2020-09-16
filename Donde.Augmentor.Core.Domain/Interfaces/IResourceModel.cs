using System;

namespace Donde.Augmentor.Core.Domain.Interfaces
{
    public interface IResourceModel
    {
        string Name { get; set; }
        Guid FileId { get; set; }
        string Extension { get; set; }
        string MimeType { get; set; }
        Guid OrganizationId { get; set; }
    }
}
