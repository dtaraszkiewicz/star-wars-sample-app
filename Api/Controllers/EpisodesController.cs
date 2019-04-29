using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsSampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : BaseController
    {
        // GET api/episodes
        [HttpGet]
        public async Task<ActionResult<ICollection<EpisodeViewModel>>> Get()
        {
            return Ok(await Mediator.Send(new GetEpisodesQuery()));
        }
    }
}
