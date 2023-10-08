using WeatherForecast.Domain.Aggregates.WeatherForecast.Values;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Commands;

namespace WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;

public interface IAddWeatherForecastCommand
{
    public DateTime DateWeatherEvent { get; }
    public string CityName { get; }
    public double Temperature { get; }
    public double HumidityInPercent { get; }
    public double DewPoint { get; }
    public int AtmospherePressure { get; }
    public IAddWindValueCommand Wind { get; }
    public double CloudinessInPercent { get; }
    public int CloudBaseInMeters { get; }
    public int HorizontalVisibilityInKilometer { get; }
    public string WeatherEvent { get; }
}
