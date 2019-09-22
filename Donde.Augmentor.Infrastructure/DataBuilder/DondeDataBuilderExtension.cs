using System;
using System.Collections.Generic;
using System.Text;
using Donde.Augmentor.Core.Domain.DataBuilder;
using Donde.Augmentor.Infrastructure.Database;

namespace Donde.Augmentor.Infrastructure.DataBuilder
{
    public static class DondeDataBuilderExtension
    {
        public static void ApplyTo(this DondeAugmentorDataBuilder builder, DondeContext context)
        {
            context.AugmentObjects.AddRange(builder.AugmentObjects);
            context.SaveChanges();
        }
    }
}
