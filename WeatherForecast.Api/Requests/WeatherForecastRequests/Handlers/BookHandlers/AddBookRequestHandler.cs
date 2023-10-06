using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.BookHandlers;

public class AddBookRequestHandler : IRequestHandler<AddBookRequest, Guid>
{
    private readonly IForecastWeatherRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public AddBookRequestHandler(IForecastWeatherRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task<Guid> Handle(AddBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var book = Book.From(request);

            if (book is null) throw new BadHttpRequestException("bad obj");

            await _bookRepo.AddAsync(book);

            await _bookRepo.UnitOfWork.SaveEntitiesAsync();

            return book.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting add new book, Ex: {ex}");

            throw;
        }
    }
}
