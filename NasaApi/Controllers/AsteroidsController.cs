using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NasaApi.Models.DTO;
using MediatR;
using NasaApi.Library.Queries;
using NasaApi.Library.Commands;

namespace NasaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        IMediator _mediator;

        public AsteroidsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: <AsteroidsController>
        [HttpGet]
        public async Task<ActionResult<List<NearEarthObjectDTO>>> Get([BindRequired, FromQuery] int days)
        {
            List<NearEarthObjectDTO> response = new List<NearEarthObjectDTO>();
            if (days < 1 || days > 7)
            {
                return StatusCode(400);
            }
            else
            {
                try
                {
                    response = await _mediator.Send(new GetNeosListQuery(days));
                }
                catch { }
                
                if (response.Any())
                {
                    return Ok(response);
                } else
                {
                    return StatusCode(204);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<NearEarthObjectDTO>>> PostTop3HazardousNeos(int days)
        {
            if (days < 1 || days > 7)
            {
                return StatusCode(400);
            }
            else
            {
                var petitionToApi = await _mediator.Send(new GetNeosListQuery(days));
                if (petitionToApi.Any())
                {
                    try
                    {
                        var insert = await _mediator.Send(new InsertNeosDatabaseCommand(petitionToApi));
                        return Ok(petitionToApi);
                    }
                    catch(Exception e)
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return StatusCode(204);
                }
            }
        }


    }
}
