﻿using WeatherForecast.Domain.Aggregates.ForecastWeather;
using MediatR;

namespace WeatherForecast.Api.Requests.Handlers.BookHandlers;

public class DeleteBookByIdRequestHandler : IRequestHandler<DeleteBookByIdRequest>
{
    private readonly IForecastWeatherRepo _bookRepo;
    private readonly ILogger<AddBookRequestHandler> _logger;

    public DeleteBookByIdRequestHandler(IForecastWeatherRepo bookRepo, ILogger<AddBookRequestHandler> logger)
    {
        _bookRepo = bookRepo;
        _logger = logger;
    }

    public async Task Handle(DeleteBookByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _bookRepo.RemoveByIdAsync(request.Id);

            await _bookRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error with attempting delete book, Ex: {ex}");

            throw;
        }
    }
}
