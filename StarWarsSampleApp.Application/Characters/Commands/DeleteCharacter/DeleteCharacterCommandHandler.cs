using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Unit>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public DeleteCharacterCommandHandler(StarWarsSampleAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Characters.Find(request.Id)
                         ?? throw new NotFoundException(typeof(Character), request.Id);

            entity.IsActive = false;

            _context.Characters.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
