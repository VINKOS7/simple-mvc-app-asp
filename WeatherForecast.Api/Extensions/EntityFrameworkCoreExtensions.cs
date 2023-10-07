﻿using Microsoft.EntityFrameworkCore;

using MediatR;

using WeatherForecast.Infrastructure;

namespace WeatherForecast.Api.Extensions;

public static class EntityFrameworkCoreExtensions
{
    public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration) => services
        .AddEntityFrameworkNpgsql()
        .AddDbContext<Context>(
            options => options
                .UseNpgsql(
                    configuration.GetConnectionString("Context"),
                    b => b.MigrationsAssembly("WeatherForecast.Api")
                )
                .EnableSensitiveDataLogging()
        );


    public static void RunMigrations(this WebApplication app, IConfiguration configuration)
    {
        var mediator = app.Services.GetRequiredService<IMediator>();
        string connectionString = configuration.GetConnectionString("Context");

        var options = new DbContextOptionsBuilder<Context>()
            .UseNpgsql(
                connectionString,
                b => b
                    .MigrationsAssembly(typeof(Program).Assembly.FullName)
                    .MigrationsHistoryTable(
                        "__EFMigrationsHistory",
                "public"))
            .Options;

        using var context = new Context(options, mediator);

        context.Database.Migrate();
    }
}