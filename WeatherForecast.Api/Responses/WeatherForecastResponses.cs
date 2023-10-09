using Newtonsoft.Json;

using WeatherForecast.Domain.Aggregates.ForecastWeather;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Enums;


namespace WeatherForecast.Api.Responses;

public class WeatherForecastResponse
{
    public WeatherForecastResponse(ForecastWeather weatherForecast)
    {
        Id = weatherForecast.Id;
        DateWeatherEvent = weatherForecast.DateWeatherEvent;
        Temperature = weatherForecast.Temperature;
        HumidityInPercent = weatherForecast.HumidityInPercent;
        DewPoint = weatherForecast.DewPoint;
        AtmospherePressure = weatherForecast.AtmospherePressure;
        Wind = new WindReadModel(weatherForecast.Wind);
        CloudBaseInMeters = weatherForecast.CloudBaseInMeters;
        CloudinessInPercent = weatherForecast.CloudinessInPercent;
        HorizontalVisibilityInKilometer = weatherForecast.HorizontalVisibilityInKilometer;
    }

    [JsonProperty("id")]
    public Guid Id { get; private set; }

    [JsonProperty("dateWeatherEvent")] 
    public DateTime DateWeatherEvent { get; private set; }

    [JsonProperty("temperature")] 
    public double Temperature { get; private set; }

    [JsonProperty("humidityInPercent")] 
    public double HumidityInPercent { get; private set; }

    [JsonProperty("dewPoint")] 
    public double DewPoint { get; private set; }

    [JsonProperty("atmospherePressure")] 
    public int AtmospherePressure { get; private set; }

    [JsonProperty("wind")] 
    public WindReadModel Wind { get; private set; }

    [JsonProperty("cloudinessInPercent")] 
    public double CloudinessInPercent { get; private set; }

    [JsonProperty("cloudBaseInMeters")] 
    public int CloudBaseInMeters { get; private set; }

    [JsonProperty("horizontalVisibilityInKilometer")] 
    public int HorizontalVisibilityInKilometer { get; private set; }

    [JsonProperty("weatherEvent")]
    public string WeatherEvent { get; private set; }
}

public record FetchWeatherForecastsFromExcelResponse([JsonProperty("weatherForecasts")] IReadOnlyCollection<WeatherForecastResponse> WeatherForecasts);

public record FetchWeatherForecastsResponse([JsonProperty("weatherForecasts")] IReadOnlyCollection<WeatherForecastResponse> WeatherForecasts);

public class WindReadModel 
{
    public WindReadModel(Wind wind)
    {
        SpeedWindInMetersPerSecond = wind.SpeedWindInMetersPerSecond;
        DirectionFirst = wind.DirectionFirst;
        DirectionSecond = wind.DirectionSecond;
    }

    [JsonProperty("speedWindInMetersPerSecond")] 
    public double SpeedWindInMetersPerSecond { get; private set; }

    [JsonProperty("directionFirst")] 
    public Direction DirectionFirst { get; private set; }

    [JsonProperty("directionSecond")] 
    public Direction DirectionSecond { get; private set; }
}
