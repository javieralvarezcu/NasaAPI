using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NasaApi.Library.Models;
using NasaApi.Library.DataAccess;
using MediatR;
using NasaApi.Library.Queries;

namespace NasaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        IMediator _mediator;

        public AsteroidsController(
            //INearEarthObjectService earthObjectService,
            IMediator mediator)
        {
            //_earthObjectService = earthObjectService;
            _mediator = mediator;
        }

        // GET: <AsteroidsController>
        [HttpGet]
        public async Task<ActionResult<List<NearEarthObjectDTO>>> Get([BindRequired, FromQuery] int days)
        {
            if (days < 1 || days > 7)
            {
                return StatusCode(400);
            }
            else
            {
                //var response = await _earthObjectService.GetAllNeosAsync(days);
                var response = await _mediator.Send(new GetNeosListQuery(days));
                if (response.Any())
                {
                    return Ok(response);
                } else
                {
                    return StatusCode(204);
                }
            }
        }


    }
}
