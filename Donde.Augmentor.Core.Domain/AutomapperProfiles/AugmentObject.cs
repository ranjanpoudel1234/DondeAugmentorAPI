using AutoMapper;
using Donde.Augmentor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donde.Augmentor.Core.Domain.AutomapperProfiles
{
    public class AugmentObjectProfile : Profile
    {
        public AugmentObjectProfile()
        {
            CreateMap<AugmentObject, AugmentObject>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.AddedDate, opts => opts.Ignore())
                .ForMember(x => x.UpdatedDate, opts => opts.Ignore())
                .ForMember(x => x.AugmentImage, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.IsDeleted, opts => opts.Ignore());

            CreateMap<AugmentObjectLocation, AugmentObjectLocation>();

            CreateMap<AugmentObjectMedia, AugmentObjectMedia>();
        }
    }
}
