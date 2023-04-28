using CommercialRoutes.Application.Dtos;
using CommercialRoutes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommercialRoutes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RoutesController : ControllerBase
{
    private readonly IRoutesAppService _routesAppService;
    private readonly ILogger<RoutesController> _logger;

    public RoutesController(
        IRoutesAppService routesAppService,
        ILogger<RoutesController> logger)
    {
        _routesAppService = routesAppService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(RouteDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetRoutes([FromQuery]string origin, [FromQuery]string destination)
    {
        try
        {
            // Tatooine", "Alderaan"
            _logger.LogInformation("GetRoutes");
            var result = await _routesAppService.GetRoute(origin, destination);
            return Ok(result);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case ArgumentException:
                    return BadRequest(exception.Message);
                case KeyNotFoundException:
                    return NotFound(exception.Message);
                default:
                {
                    _logger.LogError(exception, exception.Message);
                    return StatusCode(500);
                }
                    
            }
        }
    }

    [HttpGet("prices")]
    [ProducesResponseType(typeof(RoutePricesDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> GetRoutePrices([FromQuery]string origin, [FromQuery]string destination)
    {
        try
        {
            // Tatooine", "Alderaan"
            _logger.LogInformation("GetRoutesPrice");
            var result = await _routesAppService.GetRoutePrices(origin, destination);
            return Ok(result);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case ArgumentException:
                    return BadRequest(exception.Message);
                case KeyNotFoundException or InvalidOperationException:
                    return NotFound(exception.Message);
                default:
                {
                    _logger.LogError(exception, exception.Message);
                    return StatusCode(500);
                }
            }
        }
    }
}
