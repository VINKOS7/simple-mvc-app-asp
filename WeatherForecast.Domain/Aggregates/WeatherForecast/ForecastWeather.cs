using Dotseed.Domain;
using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;

namespace WeatherForecast.Domain.Aggregates.ForecastWeather;

public class ForecastWeather : Entity, IChangePropsWeatherForecast, IAggregateRoot
{
    public static ForecastWeather From(IAddWeatherForecastCommand command)
    {
        var book = new ForecastWeather()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Genre = command.Genre,
            Author = command.Author,
            DateOfWritten = DateTime.SpecifyKind(command.DateOfWritten, DateTimeKind.Utc)
        };

        book.SetCreatedAt(DateTime.UtcNow);
        book.SetUpdateAt(DateTime.UtcNow);

        return book;
    }

    public string Name { get; private set; }
    public string Genre { get; private set; }
    public string Author { get; private set; }
    public DateTime DateOfWritten { get; private set; }

    public void Change(IChangeWeatherForecastCommand command)
    {
        Name = command.Name;
        Genre = command.Genre;
        Author = command.Author;
        DateOfWritten = command.DateOfWritten;
    }
}
