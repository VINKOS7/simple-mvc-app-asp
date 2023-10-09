using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForecastsWeather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateWeatherEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    HumidityInPercent = table.Column<int>(type: "integer", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    AtmospherePressure = table.Column<int>(type: "integer", nullable: false),
                    Wind_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Wind_SpeedWindInMetersPerSecond = table.Column<double>(type: "double precision", nullable: false),
                    Wind_DirectionFirst = table.Column<int>(type: "integer", nullable: false),
                    Wind_DirectionSecond = table.Column<int>(type: "integer", nullable: false),
                    CloudinessInPercent = table.Column<int>(type: "integer", nullable: false),
                    CloudBaseInMeters = table.Column<int>(type: "integer", nullable: false),
                    HorizontalVisibilityInKilometer = table.Column<int>(type: "integer", nullable: false),
                    WeatherEvent = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastsWeather", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForecastsWeather");
        }
    }
}
