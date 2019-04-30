using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;

namespace StarWarsSampleApp.Api.Controllers
{
    /// <summary>
    /// Episodes API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : BaseController
    {
        /// <summary>
        /// Gets all episodes
        /// </summary>
        /// <returns>List of Episode View Model</returns>
        [HttpGet]
        public async Task<ActionResult<IList<EpisodeViewModel>>> Get()
        {
            return Ok(await Mediator.Send(new GetEpisodesQuery()));
        }

        /// <summary>
        /// Creates a new episode
        /// </summary>
        /// <remarks>
        /// Sample create Episode:
        /// 
        ///     POST api/episodes
        ///     {
        ///         "name": "episode name"
        ///     }
        ///     
        /// </remarks>
        /// <param name="command">Episode to create</param>
        /// <returns>The created episode id</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateEpisodeCommand command)
        {
            var episodeId = await Mediator.Send(command);
            return Ok(episodeId);
        }
    }
}
