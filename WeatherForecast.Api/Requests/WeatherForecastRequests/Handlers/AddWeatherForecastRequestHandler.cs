using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.WeatherForecastHandlers;

public class AddWeatherForecastRequestHandler : IRequestHandler<AddWeatherForecastRequest, Guid>
{
    private readonly IForecastWeatherRepo _weatherForecastRepo;
    private readonly ILogger<AddWeatherForecastRequestHandler> _logger;

    public AddWeatherForecastRequestHandler(IForecastWeatherRepo weatherForecastRepo, ILogger<AddWeatherForecastRequestHandler> logger)
    {
        _weatherForecastRepo = weatherForecastRepo;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var book = ForecastWeather.From(request);

            if (book is null) throw new BadHttpRequestException("bad obj");

            await _weatherForecastRepo.AddAsync(book);

            await _weatherForecastRepo.UnitOfWork.SaveEntitiesAsync();

            return book.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting add new book, Ex: {ex}");

            throw;
        }
    }
}
