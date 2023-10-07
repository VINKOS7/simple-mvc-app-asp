using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using WeatherForecast.Api.Requests;
using WeatherForecast.Api.Responses;

namespace WeatherForecast.Api.Controllers;

[Route("WeatherForecast")]
public class WeatherForecastController : Controller
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator) => _mediator = mediator;
    

    [AllowAnonymous, HttpGet("fetch")]
    public async Task<FetchWeatherForecastsResponse> Fetch([FromQuery] int offset = 0, [FromQuery] int size = 20) => await _mediator.Send(new FetchWeatherForecastsRequest(offset, size));


    [Authorize, HttpPost("add")]
    public async Task<Guid> Add([FromBody] AddWeatherForecastRequest request) => await _mediator.Send(request);


    [Authorize, HttpPost("change")]
    public async Task Change([FromBody] ChangeWeatherForecastRequest request) => await _mediator.Send(request);


    [Authorize, HttpDelete("delete")]
    public async Task Delete([FromQuery] Guid id) => await _mediator.Send(new DeleteWeatherForecastByIdRequest(id));
}
