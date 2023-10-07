using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.WeatherForecastHandlers;

public class FetchWeatherForecastsRequestHandler : IRequestHandler<FetchWeatherForecastsRequest, FetchWeatherForecastsResponse>
{
    private readonly IForecastWeatherRepo _weatherForecastRepo;
    private readonly ILogger<AddWeatherForecastRequestHandler> _logger;

    public FetchWeatherForecastsRequestHandler(IForecastWeatherRepo weatherForecastRepo, ILogger<AddWeatherForecastRequestHandler> logger)
    {
        _weatherForecastRepo = weatherForecastRepo;
        _logger = logger;
    }

    public async Task<FetchWeatherForecastsResponse> Handle(FetchWeatherForecastsRequest request, CancellationToken cancellationToken)
    {
        try
        { 
            var weatherForecasts = await _weatherForecastRepo.FetchAsync(request.Offset, request.Size);

            return new FetchWeatherForecastsResponse(weatherForecasts.Select(b => new WeatherForecastResponse(b)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting fetch books, Ex: {ex}");

            throw;
        }
    }
}
