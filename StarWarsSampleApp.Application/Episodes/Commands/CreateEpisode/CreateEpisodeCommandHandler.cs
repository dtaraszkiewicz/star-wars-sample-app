using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeCommandHandler : IRequestHandler<CreateEpisodeCommand, int>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public CreateEpisodeCommandHandler(StarWarsSampleAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEpisodeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Episode { Name = request.Name };

            _context.Episodes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
