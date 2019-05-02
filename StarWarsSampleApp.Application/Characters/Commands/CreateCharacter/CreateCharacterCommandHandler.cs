using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, int>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateCharacterCommandHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //TODO Refactor handler
        public async Task<int> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateCharacterCommand, Character>(request);
            var episodes = GetCharacterEpisodes(request.EpisodesIds);
            var friends = GetCharacterFriends(request.FriendsIds);

            entity = AttachEpisodesAndFriends(episodes, friends, entity);

            _context.Characters.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            UpdateFriends(entity);

            return entity.Id;
        }

        private List<Episode> GetCharacterEpisodes(int[] episodeIds)
        {
            return _context.Episodes.Where(x => episodeIds.Contains(x.Id)).ToList();
        }

        private List<Character> GetCharacterFriends(int[] friendIds)
        {
            return _context.Characters.Where(x => friendIds.Contains(x.Id)).ToList();
        }

        private Character AttachEpisodesAndFriends(List<Episode> episodes, List<Character> friends, Character entity)
        {
            foreach (var episode in episodes)
            {
                entity.Episodes.Add(new CharacterEpisode{Episode = episode});
            }

            foreach (var friend in friends)
            {
                entity.Friends.Add(new Friendship{Friend = friend});
            }

            return entity;
        }

        private void UpdateFriends(Character character)
        {
            foreach (var friendship in character.Friends)
            {
                _context.Friendships.Add(new Friendship {Character = friendship.Friend, Friend = character});
            }

            _context.SaveChanges();
        }
    }
}
