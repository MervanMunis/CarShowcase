using AutoMapper;
using WebAPI.DTOs.Brand;
using WebAPI.DTOs.Car;
using WebAPI.DTOs.Feature;
using WebAPI.DTOs.Model;
using WebAPI.Models;

namespace WebAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Brand mappings
            CreateMap<Brand, BrandResponse>().ReverseMap();
            CreateMap<BrandRequest, Brand>();

            // Car mappings
            CreateMap<Car, CarResponse>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model!.Name))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.CarFeatures!.Select(cf => cf.Feature).ToList()));
            CreateMap<CarRequest, Car>();

            // Feature mappings
            CreateMap<Feature, FeatureResponse>().ReverseMap();
            CreateMap<FeatureRequest, Feature>();

            // Model mappings
            CreateMap<Model, ModelResponse>().ReverseMap();
            CreateMap<ModelRequest, Model>();   
        }
    }
}
