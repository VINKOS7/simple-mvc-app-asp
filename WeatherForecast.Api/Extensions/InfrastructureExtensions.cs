using WeatherForecast.Domain.Aggregates.Account;
using WeatherForecast.Domain.Aggregates.ForecastWeather;
using WeatherForecast.Infrastructure;

namespace WeatherForecast.Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services) => services
        .AddScoped<IForecastWeatherRepo, ForecastWeatherRepo>()
        .AddScoped<IAccountRepo, AccountRepo>();
    
}
