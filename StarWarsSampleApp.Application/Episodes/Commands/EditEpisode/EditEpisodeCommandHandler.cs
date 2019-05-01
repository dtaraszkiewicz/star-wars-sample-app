using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommandHandler : IRequestHandler<EditEpisodeCommand, int>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public EditEpisodeCommandHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Episodes.Find(request.Id)
                ?? throw new NotFoundException(typeof(Episode), request.Id);

            entity = _mapper.Map(request, entity);

            _context.Episodes.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
            
            return entity.Id;
        }
    }
}
