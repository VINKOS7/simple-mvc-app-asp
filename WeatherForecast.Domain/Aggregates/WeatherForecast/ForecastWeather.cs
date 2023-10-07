using Dotseed.Domain;
using System.Diagnostics.Metrics;
using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values;

namespace WeatherForecast.Domain.Aggregates.ForecastWeather;

public class ForecastWeather : Entity, IAggregateRoot
{
    public static ForecastWeather From(IAddWeatherForecastCommand command)
    {
        var forecastWeather = new ForecastWeather()
        {
            Id = Guid.NewGuid(),
            DateWeatherEvent = command.DateWeatherEvent,
            Temperature = command.Temperature,
            HumidityInPercent = command.HumidityInPercent,
            DewPoint = command.DewPoint,
            AtmospherePressure = command.AtmospherePressure,
            Wind = Wind.From(command.Wind),
            CloudBaseInMeters = command.CloudBaseInMeters,
            CloudinessInPercent = command.CloudinessInPercent,
            HorizontalVisibilityInKilometer = command.HorizontalVisibilityInKilometer
        };

        forecastWeather.SetCreatedAt(DateTime.UtcNow);
        forecastWeather.SetUpdateAt(DateTime.UtcNow);

        return forecastWeather;
    }

    public DateTime DateWeatherEvent { get; private set; }
    public double Temperature { get; private set; }
    public double HumidityInPercent { get => HumidityInPercent; set => _ = value <= 100 ? value : 100; }
    public double DewPoint { get; private set; }
    public int AtmospherePressure { get; private set; }
    public Wind Wind { get; private set; }
    public double CloudinessInPercent { get => CloudinessInPercent; set => _ = value <= 100 ? value : 100; }
    public int CloudBaseInMeters { get; private set; }
    public int HorizontalVisibilityInKilometer { get; private set; }
    public string WeatherEvent { get; private set; }

    public void Change(IChangeWeatherForecastCommand command)
    {
        DateWeatherEvent = command.DateWeatherEvent;
        Temperature = command.Temperature;
        HumidityInPercent = command.HumidityInPercent;
        DewPoint = command.DewPoint;
        AtmospherePressure = command.AtmospherePressure;
        WeatherEvent = command.WeatherEvent;
        Wind = Wind.From(command.Wind);
        CloudinessInPercent = command.CloudinessInPercent;
        CloudBaseInMeters = command.CloudBaseInMeters;
        HorizontalVisibilityInKilometer = command.HorizontalVisibilityInKilometer;
        WeatherEvent = command.WeatherEvent;
    }
}