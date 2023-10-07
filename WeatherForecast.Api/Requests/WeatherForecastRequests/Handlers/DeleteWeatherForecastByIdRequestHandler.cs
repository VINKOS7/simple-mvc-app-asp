using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.WeatherForecastHandlers;

public class DeleteWeatherForecastByIdRequestHandler : IRequestHandler<DeleteWeatherForecastByIdRequest>
{
    private readonly IForecastWeatherRepo _weatherForecastRepo;
    private readonly ILogger<AddWeatherForecastRequestHandler> _logger;

    public DeleteWeatherForecastByIdRequestHandler(IForecastWeatherRepo weatherForecastRepo, ILogger<AddWeatherForecastRequestHandler> logger)
    {
        _weatherForecastRepo = weatherForecastRepo;
        _logger = logger;
    }

    public async Task Handle(DeleteWeatherForecastByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _weatherForecastRepo.RemoveByIdAsync(request.Id);

            await _weatherForecastRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting delete book, Ex: {ex}");

            throw;
        }
    }
}
