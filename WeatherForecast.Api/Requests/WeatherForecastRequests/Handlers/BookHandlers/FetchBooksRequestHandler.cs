using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.BookHandlers;

public class FetchBooksRequestHandler : IRequestHandler<FetchBooksRequest, FetchBooksResponse>
{
    private readonly IForecastWeatherRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public FetchBooksRequestHandler(IForecastWeatherRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task<FetchBooksResponse> Handle(FetchBooksRequest request, CancellationToken cancellationToken)
    {
        try
        { 
            var books = await _bookRepo.FetchAsync(request.Offset, request.Size);

            return new FetchBooksResponse(books.Select(b => new BookResponse(b)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting fetch books, Ex: {ex}");

            throw;
        }
    }
}
