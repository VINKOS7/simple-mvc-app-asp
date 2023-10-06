using Microsoft.EntityFrameworkCore;

using MediatR;
using Dotseed.Context;

using WeatherForecast.Domain.Aggregates.ForecastWeather;
using WeatherForecast.Domain.Aggregates.Account;
using WeatherForecast.Infrastructure.EntityConfigures;

namespace WeatherForecast.Infrastructure;

public class Context : UnitOfWorkContext
{
    public DbSet<Domain.Aggregates.ForecastWeather.ForecastWeather> ForecastsWeather { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public Context(DbContextOptions options, IMediator mediator) : base(options, mediator) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountEntityConfig());
    }
}