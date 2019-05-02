using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWarsSampleApp.Application.Characters.Queries;

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
    }
}
