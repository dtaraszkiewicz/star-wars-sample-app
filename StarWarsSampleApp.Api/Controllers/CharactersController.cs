using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Characters.Commands.CreateCharacter;
using StarWarsSampleApp.Application.Characters.Commands.EditCharacter;
using StarWarsSampleApp.Application.Characters.Queries;
using StarWarsSampleApp.Application.Characters.Queries.GetCharacters;

namespace StarWarsSampleApp.Api.Controllers
{
    /// <summary>
    /// Episodes API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : BaseController
    {
        /// <summary>
        /// Get character by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Character View Model</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterViewModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCharacterQuery {Id = id}));
        }

        /// <summary>
        /// Get all characters
        /// </summary>
        /// <returns>Collection of Get Character View Model</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetCharacterViewModel>>> Get()
        {
            return Ok(await Mediator.Send(new GetCharactersQuery()));
        }

        /// <summary>
        /// Creates new character
        /// </summary>
        /// <param name="command"></param>
        /// <returns>id of created character</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create([FromBody] CreateCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Updates character
        /// </summary>
        /// <param name="command"></param>
        /// <returns>id of updated character</returns>
        [HttpPost("Edit")]
        public async Task<ActionResult<int>> Edit([FromBody] EditCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
