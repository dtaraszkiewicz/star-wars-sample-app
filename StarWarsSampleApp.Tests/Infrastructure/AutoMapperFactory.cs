using AutoMapper;
using StarWarsSampleApp.Application.Infrastructure.Automapper;

namespace StarWarsSampleApp.Tests.Infrastructure
{
    public class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));

            return mapperConfig.CreateMapper();
        }
    }
}
