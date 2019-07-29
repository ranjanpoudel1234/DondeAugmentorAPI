using System;

namespace Donde.Augmentor.Core.Domain.Interfaces
{
    public interface IAuditFieldsModel
    {
        DateTime AddedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool IsActive { get; set; }
    }
}
