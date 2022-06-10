using System;
using AutoMapper;
using WineDocumentation.Core.Domain;
using WineDocumentation.Infrastructure.DTO;

namespace WineDocumentation.Infrastructure.Mapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Wine, WineDto>();
            })
            .CreateMapper();
    }
}