using Donde.Augmentor.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donde.Augmentor.Core.Domain.Models
{
    public class Organization : IDondeModel, IAuditFieldsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
