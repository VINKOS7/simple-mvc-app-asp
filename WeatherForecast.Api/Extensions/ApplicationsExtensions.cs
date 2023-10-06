using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration) => services
        .AddScoped<IEmailService, EmailService>();
}
