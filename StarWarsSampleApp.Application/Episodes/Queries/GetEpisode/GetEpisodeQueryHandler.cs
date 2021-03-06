﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisode
{
    public class GetEpisodeQueryHandler : IRequestHandler<GetEpisodeQuery, EpisodeViewModel>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public GetEpisodeQueryHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EpisodeViewModel> Handle(GetEpisodeQuery request, CancellationToken cancellationToken)
        {
            var episode = await _context.Episodes.FindAsync(request.Id);

            if (episode == null || episode.IsActive == false)
            {
                throw new NotFoundException(typeof(Episode), request.Id);
            }

            var result = _mapper.Map<EpisodeViewModel>(episode);

            return result;
        }
    }
}
