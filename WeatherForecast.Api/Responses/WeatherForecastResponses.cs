using Newtonsoft.Json;

using WeatherForecast.Domain.Aggregates.ForecastWeather;


namespace WeatherForecast.Api.Responses;

public class WeatherForecastResponse
{
    public WeatherForecastResponse(ForecastWeather weatherForecast)
    {
        Id = weatherForecast.Id;
        Name = weatherForecast.Name;
        Genre = weatherForecast.Genre;
        Author = weatherForecast.Author;
        DateOfWritten = weatherForecast.DateOfWritten;
    }

    [JsonProperty("id")]
    public Guid Id { get; private set; }

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("genre")]
    public string Genre { get; private set; }

    [JsonProperty("name")]
    public string Author { get; private set; }

    [JsonProperty("dateOfWritten")]
    public DateTime DateOfWritten { get; private set; }
}

public record FetchWeatherForecastsResponse([JsonProperty("books")] ICollection<WeatherForecastResponse> Books);