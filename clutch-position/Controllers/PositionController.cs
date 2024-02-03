using clutch_position.Extensions;
using clutch_position.Requests;
using clutch_position.Resources;
using clutch_position.Services;
using Microsoft.AspNetCore.Mvc;

namespace clutch_position.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult PostPosition(PostPositionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();
            _positionService.CreatePosition(request);
           var response =  request.FromAddPositionRequest();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetPositions()
        {
            var resource = await _positionService.GetPositionsAync();
            return Ok(resource);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetPosition(string id)
        {
            var resource = await _positionService.GetPositionAsync(id);
            return Ok(resource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PositionResource))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPosition(string id, [FromBody] PutPositionRequest request)
        {
            await _positionService.AmendPosition(request, id);
            return Ok($"position with id {id} updated");
        }
    }
}

