using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NasaApi.Models;
using NasaApi.Services;
using Newtonsoft.Json;

namespace NasaApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        INearEarthObjectService _earthObjectService;

        public AsteroidsController(INearEarthObjectService earthObjectService)
        {
            _earthObjectService = earthObjectService;
        }

        // GET: <AsteroidsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NearEarthObjectDTO>>> Get([BindRequired, FromQuery] int days)
        {
            if (days < 1 || days > 7)
            {
                return StatusCode(400);
            }
            else
            {
                //var response = await Task.Run(() => _earthObjectService.GetAllNeosAsync(days));
                var response = await _earthObjectService.GetAllNeosAsync(days);
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
