using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donde.Augmentor.Core.Domain.Dto
{
    /// <summary>
    /// This class stores any extra information we want to transfer along with augment main object.
    /// </summary>
    public class AugmentObjectDto : AugmentObject
    {
        public double Distance { get; set; }

        public string AudioName { get; set; }
        public string AudioUrl { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
