using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Enums;

namespace WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Commands;

public interface IAddWindValueCommand
{
    public double SpeedWindInMetersPerSecond { get; }
    public Direction DirectionFirst { get => DirectionFirst; }
    public Direction DirectionSecond { get => DirectionSecond; }
}
