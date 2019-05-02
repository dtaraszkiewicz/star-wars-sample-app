using System.Collections.Generic;
using AutoMapper;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Interfaces.Mapping;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Application.Characters.Queries
{
    public class GetCharacterViewModel : IHaveCustomMapping
    {
        public string Name { get; set; }
        public List<EpisodeViewModel> Episodes { get; set; }
        public List<FriendViewModel> Friends { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.AllowNullCollections = true;

            configuration.CreateMap<CharacterEpisode, EpisodeViewModel>()
                .ForMember(x => x.Name, s => s.MapFrom(x => x.Episode.Name));

            configuration.CreateMap<Friendship, FriendViewModel>()
                .ForMember(x => x.Name, s => s.MapFrom(x => x.Friend.Name));

            configuration.CreateMap<Character, GetCharacterViewModel>()
                .ForMember(x => x.Name, s => s.MapFrom(x => x.Name))
                .ForMember(x => x.Episodes, s => s.MapFrom(x => x.Episodes))
                .ForMember(x => x.Friends, s => s.MapFrom(x => x.Friends));
        }
    }

    public class FriendViewModel
    {
        public string Name { get; set; }
    }
}
