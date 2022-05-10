using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NasaApi.Library.Queries;

namespace NasaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        IMediator _mediator;

        public PaypalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PaypalController>
        [HttpGet]
        public async Task<IActionResult> Get([BindRequired, FromQuery] DateTimeOffset start_date, [BindRequired, FromQuery] DateTimeOffset end_date)
        {
            string response;

            if (start_date > end_date)
            {
                Response.Headers.Add("Error", "La primera fecha debe ser menor a la segunda");
                return BadRequest();
            }
            else if(end_date.Subtract(start_date).Days > 31)
            {
                Response.Headers.Add("Error", "El rango de fechas no puede ser mayor de 31 dias");
                return BadRequest();
            }
            else
            {
                try
                {
                    response = await _mediator.Send(new GetTransactionsQuery(start_date, end_date));
                    return Ok(response);
                }
                catch (Exception)
                {
                    Response.Headers.Add("Error", "Fallo al recibir datos de Paypal");
                    return StatusCode(500);
                }
            }
        }
    }
}
