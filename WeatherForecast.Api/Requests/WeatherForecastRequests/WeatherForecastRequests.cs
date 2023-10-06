using MediatR;
using Newtonsoft.Json;

using WeatherForecast.Domain.Aggregates.ForecastWeather;
using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;
using WeatherForecast.Api.Responses;

namespace WeatherForecast.Api.Requests;

public record AddWeatherForecastRequest(
    [JsonProperty("name")] string Name,
    [JsonProperty("genre")] string Genre,
    [JsonProperty("author")] string Author,
    [JsonProperty("dateOfWritten")] DateTime DateOfWritten
)
: IAddWeatherForecastCommand, IRequest<Guid>;

public record ChangeWeatherForecastRequest(
    [JsonProperty("id")] Guid Id,
    [JsonProperty("name")] string Name,
    [JsonProperty("genre")] string Genre,
    [JsonProperty("author")] string Author,
    [JsonProperty("dateOfWritten")] DateTime DateOfWritten
)
: IChangeWeatherForecastCommand, IRequest;

public record FetchBooksRequest(
    [JsonProperty("offset")] int Offset,
    [JsonProperty("size")] int Size
) 
: IRequest<FetchWeatherForecastsResponse>;

public record DeleteWeatherForecastByIdRequest([JsonProperty("id")] Guid Id) : IRequest;