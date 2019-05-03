using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Commands.EditCharacter
{
    public class EditCharacterCommandHandler : IRequestHandler<EditCharacterCommand, int>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public EditCharacterCommandHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //TODO Refactor handler
        public async Task<int> Handle(EditCharacterCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Characters
                .Include(x => x.Episodes)
                .Include(x => x.Friends)
                .SingleOrDefault(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(typeof(Character), request.Id);
            }

            entity = UpdateCharacter(entity, request);

            _context.Characters.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        private Character UpdateCharacter(Character character, EditCharacterCommand request)
        {
            var episodesToAdd = _context.Episodes.Where(x => request.EpisodesIds.Contains(x.Id));
            character.Episodes.Clear();
            foreach (var episode in episodesToAdd)
            {
                character.Episodes.Add(new CharacterEpisode{Episode = episode});
            }

            var friendsToAdd = _context.Characters.Where(x => request.FriendsIds.Contains(x.Id)).ToList();
            foreach (var friendship in character.Friends)
            {
                if (friendsToAdd.Any(x => x.Id == friendship.FriendId))
                {
                    friendsToAdd.Remove(friendsToAdd.Single(x => x.Id == friendship.FriendId));
                }
            }

            foreach (var friend in friendsToAdd)
            {
                character.Friends.Add(new Friendship{Friend = friend});
            }

            character.Name = request.Name;

            return character;
        }
    }
}
