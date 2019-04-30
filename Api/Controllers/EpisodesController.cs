using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;

namespace StarWarsSampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : BaseController
    {
        // GET api/episodes
        [HttpGet]
        public async Task<ActionResult<IList<EpisodeViewModel>>> Get()
        {
            return Ok(await Mediator.Send(new GetEpisodesQuery()));
        }

        // POST api/episodes
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateEpisodeCommand command)
        {
            var episodeId = await Mediator.Send(command);
            return Ok(episodeId);
        }
    }
}
