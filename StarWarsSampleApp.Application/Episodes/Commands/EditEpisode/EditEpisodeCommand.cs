using AutoMapper;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;
using StarWarsSampleApp.Application.Interfaces.Mapping;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Application.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommand : CreateEpisodeCommand, IHaveCustomMapping
    {
        public int Id { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EditEpisodeCommand, Episode>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
        }
    }
}
