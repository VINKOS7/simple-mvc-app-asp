using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.WeatherForecastHandlers;

public class AddFromExсelWeatherForecastRequestHandler : IRequestHandler<AddWeatherForecastFromExelRequest>
{
    private readonly IForecastWeatherRepo _weatherForecastRepo;
    private readonly ILogger<AddWeatherForecastRequestHandler> _logger;

    public AddFromExсelWeatherForecastRequestHandler(IForecastWeatherRepo weatherForecastRepo, ILogger<AddWeatherForecastRequestHandler> logger)
    {
        _weatherForecastRepo = weatherForecastRepo;
        _logger = logger;
    }

    public async Task Handle(AddWeatherForecastFromExelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var weatherForecasts = ForecastWeather.From(request);

            if (weatherForecasts is null) throw new BadHttpRequestException("bad obj");

            foreach (var weatherForecast in weatherForecasts) await _weatherForecastRepo.AddNotDoubleByDateAsync(weatherForecast);

            await _weatherForecastRepo.UnitOfWork.SaveEntitiesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting add new book, Ex: {ex}");

            throw;
        }
    }
}