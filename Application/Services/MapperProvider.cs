using Application.DTO.PhoneSpecificationsAPI.ListBrands;
using Application.DTO.PhoneSpecificationsAPI.PhoneSpecifications;
using Application.Interfaces;
using AutoMapper;
using Models.Entities;
using Newtonsoft.Json;

namespace Application.Services
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;
        public IMapper GetMapper() => _mapper;

        public MapperProvider()
        {
            _mapper = Initialize();
        }

        private IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    //cfg.CreateMap<TO, FROM>

                    cfg.CreateMap<BrandDto, Brand>()
                        .ForMember(x => x.Slug,
                            m => m.MapFrom(x => x.Brand_slug))
                        .ForMember(x => x.Name,
                            m => m.MapFrom(x => x.Brand_name))
                        .ForAllOtherMembers(m => m.Ignore());

                    cfg.CreateMap<PhoneSpecificationsDto, Phone>()
                        .ForMember(x => x.PhoneName, m => m.MapFrom(x => x.Data.Phone_name))
                        .ForMember(x => x.Dimension, m => m.MapFrom(x => x.Data.Dimension))
                        .ForMember(x => x.Os, m => m.MapFrom(x => x.Data.Os))
                        .ForMember(x => x.Storage, m => m.MapFrom(x => x.Data.Storage))
                        .ForMember(x => x.Thumbnail, m => m.MapFrom(x => x.Data.Thumbnail))
                        .ForMember(x => x.ReleaseDate, m => m.MapFrom(x => x.Data.Release_date))
                        .ForMember(x => x.Images,
                            m => m.MapFrom(x =>
                                JsonConvert.SerializeObject(x.Data.Phone_images, Formatting.Indented)))
                        .ForMember(x => x.Specifications,
                            m => m.MapFrom(x =>
                                JsonConvert.SerializeObject(x.Data.Specifications, Formatting.None)))
                        .ForAllOtherMembers(m => m.Ignore());
                }
            );
            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}