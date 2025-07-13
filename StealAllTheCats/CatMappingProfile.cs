using AutoMapper;
using StealAllTheCats.Models;
using StealAllTheCats.Models.Api;
using StealAllTheCats.Models.Dto;

namespace StealAllTheCats
{
    public class CatMappingProfile : Profile
    {
        public CatMappingProfile()
        {
            CreateMap<CatApiModel, CatEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CatId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom<TemperamentResolver>());

            CreateMap<CatEntity, CatDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CatId, opt => opt.MapFrom(src => src.CatId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<TagEntity, TagDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created));
        }
    }
}
