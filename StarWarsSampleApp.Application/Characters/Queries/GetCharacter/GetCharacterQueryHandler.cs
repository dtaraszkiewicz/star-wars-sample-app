using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Queries.GetCharacter
{
    public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, GetCharacterViewModel>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCharacterQueryHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCharacterViewModel> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Characters
                .Where(x => x.Id == request.Id && x.IsActive.Value)
                .Include(x => x.Episodes)
                .ThenInclude(e => e.Episode)
                .Include(x => x.Friends)
                .ThenInclude(f => f.Friend)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(typeof(Character), request.Id);
            }
            
            var result = _mapper.Map<GetCharacterViewModel>(entity);

            return result;
        }
    }
}
