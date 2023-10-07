using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.WeatherForecastHandlers;

public class ChangeWeatherForecastRequestHandler : IRequestHandler<ChangeWeatherForecastRequest>
{
    private readonly IForecastWeatherRepo _weatherForecastRepo;
    private readonly ILogger<ChangeWeatherForecastRequestHandler> _logger;

    public ChangeWeatherForecastRequestHandler(IForecastWeatherRepo weatherForecastRepo, ILogger<ChangeWeatherForecastRequestHandler> logger)
    {
        _weatherForecastRepo = weatherForecastRepo;
        _logger = logger;
    }

    public async Task Handle(ChangeWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var weatherForecast = await _weatherForecastRepo.FindByIdAsync(request.Id);

            if (weatherForecast is null) throw new BadHttpRequestException("not found weather forecast");

            weatherForecast.Change(request);

            await _weatherForecastRepo.UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting change book, Ex: {ex}");

            throw;
        }
    }
}
