using System.Collections.Generic;
using Donde.Augmentor.Core.Domain.Models;

namespace Donde.Augmentor.Core.Domain.DataBuilder
{
    public class BaseDataBuilder
    {
        public List<AugmentObject> AugmentObjects { get; set; } = new List<AugmentObject>();
    }
}
