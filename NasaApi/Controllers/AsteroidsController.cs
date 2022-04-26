using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NasaApi.Library.Commands;
using NasaApi.Library.Queries;
using NasaApi.Models.DTO;

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

        // GET: asteroids?days=[days]
        [HttpGet]
        public async Task<IActionResult> Get([BindRequired, FromQuery] int days)
        {
            List<NearEarthObjectDTO> response = new List<NearEarthObjectDTO>();
            if (days < 1 || days > 7)
            {
                Response.Headers.Add("Error", "El valor del parametro debe ser entre 1 y 7");
                return BadRequest();
            }
            else
            {
                try
                {
                    response = await _mediator.Send(new GetNeosListQuery(days));
                }
                catch (Exception e){
                    Response.Headers.Add("Error", "Fallo al recibir datos de la NASA");
                    return StatusCode(500);
                }

                if (response.Any())
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(204);
                }
            }
        }

        // POST: asteroids?days=[days]
        [HttpPost]
        public async Task<IActionResult> PostTop3HazardousNeos(int days)
        {
            List<NearEarthObjectDTO> response = new List<NearEarthObjectDTO>();
            if (days < 1 || days > 7)
            {
                Response.Headers.Add("Error", "El valor del parametro debe ser entre 1 y 7");
                return BadRequest();
            }
            else
            {
                try
                {
                    response = await _mediator.Send(new GetNeosListQuery(days));
                }
                catch(Exception e)
                {
                    Response.Headers.Add("Error", "Fallo al recibir datos de la NASA");
                    return StatusCode(500);
                }
                
                if (response.Any())
                {
                    try
                    {
                        var insert = await _mediator.Send(new InsertNeosDatabaseCommand(response));
                        Response.Headers.Add("Success", "Insertados registros no duplicados en la base de datos");
                        return Ok(response);
                    }
                    catch (Exception e)
                    {
                        Response.Headers.Add("Error", "Fallo al insertar en la base de datos");
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
