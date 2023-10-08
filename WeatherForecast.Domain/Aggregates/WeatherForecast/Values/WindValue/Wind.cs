using Dotseed.Domain;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Enums;

namespace WeatherForecast.Domain.Aggregates.WeatherForecast.Values;

public class Wind : ValueObject
{
    public static Wind From(IAddWindValueCommand command) => new()
    {
        Id = Guid.NewGuid(),
        SpeedWindInMetersPerSecond = command.SpeedWindInMetersPerSecond,
        DirectionFirst = command.DirectionFirst,
        DirectionSecond = command.DirectionSecond
    };

    public Guid Id { get; set; }
    public double SpeedWindInMetersPerSecond { get; set; }
    public Direction DirectionFirst { get; init; }
    public Direction DirectionSecond { get => DirectionSecond; init => _ = value is not Direction.Calm ? value : Direction.Calm; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id;
        yield return SpeedWindInMetersPerSecond;
        yield return DirectionFirst;
        yield return DirectionSecond;
    }
}
