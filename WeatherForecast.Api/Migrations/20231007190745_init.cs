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
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessStatus = table.Column<int>(type: "integer", nullable: false),
                    ActivationCode = table.Column<string>(type: "text", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    EmailAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    PhoneNumberAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wind",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SpeedWindInMetersPerSecond = table.Column<double>(type: "double precision", nullable: false),
                    DirectionFirst = table.Column<int>(type: "integer", nullable: false),
                    DirectionSecond = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wind", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    OnlineAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => new { x.AccountId, x.Id });
                    table.ForeignKey(
                        name: "FK_Device_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForecastsWeather",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateWeatherEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    HumidityInPercent = table.Column<double>(type: "double precision", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    AtmospherePressure = table.Column<int>(type: "integer", nullable: false),
                    WindId = table.Column<Guid>(type: "uuid", nullable: false),
                    CloudinessInPercent = table.Column<double>(type: "double precision", nullable: false),
                    CloudBaseInMeters = table.Column<int>(type: "integer", nullable: false),
                    HorizontalVisibilityInKilometer = table.Column<int>(type: "integer", nullable: false),
                    WeatherEvent = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastsWeather", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForecastsWeather_Wind_WindId",
                        column: x => x.WindId,
                        principalTable: "Wind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForecastsWeather_WindId",
                table: "ForecastsWeather",
                column: "WindId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "ForecastsWeather");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Wind");
        }
    }
}
