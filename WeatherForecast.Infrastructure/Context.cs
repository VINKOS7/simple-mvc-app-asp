using Microsoft.EntityFrameworkCore;

using MediatR;
using Dotseed.Context;

using WeatherForecast.Domain.Aggregates.ForecastWeather;
using WeatherForecast.Infrastructure.EntityConfigures;

namespace WeatherForecast.Infrastructure;

public class Context : UnitOfWorkContext
{
    public DbSet<ForecastWeather> ForecastsWeather { get; set; }

    public Context(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ForecastWeatherConfig());
    }
}