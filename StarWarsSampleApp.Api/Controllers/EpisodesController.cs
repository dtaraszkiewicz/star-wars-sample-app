using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;
using StarWarsSampleApp.Application.Episodes.Commands.DeleteEpisode;
using StarWarsSampleApp.Application.Episodes.Commands.EditEpisode;

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
        /// Get episode using id
        /// </summary>
        /// <param name="id">episode id</param>
        /// <returns>Episode View Model</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EpisodeViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetEpisodeQuery { Id = id }));
        }

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
        ///     POST api/episodes/create
        ///     {
        ///         "name": "episode name"
        ///     }
        ///     
        /// </remarks>
        /// <param name="command">Episode to create</param>
        /// <returns>The created episode id</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateEpisodeCommand command)
        {
            var episodeId = await Mediator.Send(command);
            return Ok(episodeId);
        }

        /// <summary>
        /// Updates restaurant
        /// </summary>
        /// <remarks>
        /// Sample edit Episode:
        /// 
        ///     POST api/episodes/edit
        ///     {
        ///         "id": "1",
        ///         "name": "episode name"
        ///     }
        ///     
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public async Task<ActionResult<int>> Edit([FromBody] EditEpisodeCommand command)
        {
            var episodeId = await Mediator.Send(command);
            return Ok(episodeId);
        }

        /// <summary>
        /// Deletes episode
        /// </summary>
        /// <param name="id"></param>
        /// <returns>204 NoContent</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEpisodeCommand {Id = id});
            return NoContent();
        }
    }
}
