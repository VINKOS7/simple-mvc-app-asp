namespace WeatherForecast.Domain.Aggregates.ForecastWeather;

public interface IChangePropsWeatherForecast
{
    public string Name { get;}
    public string Genre { get; }
    public string Author { get; }
    public DateTime DateOfWritten { get; }
}
