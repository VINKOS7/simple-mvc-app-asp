using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WeatherForecast.Domain.Aggregates.ForecastWeather;

namespace WeatherForecast.Infrastructure.EntityConfigures;

public class ForecastWeatherConfig
{
    public void Configure(EntityTypeBuilder<ForecastWeather> builder) => builder
        .OwnsOne(wf => wf.Wind);
}
