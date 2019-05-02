using AutoMapper;
using MediatR;
using StarWarsSampleApp.Application.Interfaces.Mapping;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest<int>, IHaveCustomMapping
    {
        public string Name { get; set; }
        public int[] EpisodesIds { get; set; }
        public int[] FriendsIds { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateCharacterCommand, Character>()
                .ForMember(x => x.Name, s => s.MapFrom(x => x.Name));
        }
    }
}
