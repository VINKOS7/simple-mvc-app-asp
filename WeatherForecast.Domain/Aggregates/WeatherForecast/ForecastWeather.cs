using Dotseed.Domain;
using MathNet.Numerics;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.UserModel;
using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Enums;

namespace WeatherForecast.Domain.Aggregates.ForecastWeather;

public class ForecastWeather : Entity, IAggregateRoot
{
    public static ForecastWeather From(IAddWeatherForecastCommand command)
    {
        var forecastWeather = new ForecastWeather()
        {
            Id = Guid.NewGuid(),
            CityName = command.CityName,
            DateWeatherEvent = command.DateWeatherEvent,
            Temperature = command.Temperature,
            HumidityInPercent = command.HumidityInPercent,
            DewPoint = command.DewPoint,
            AtmospherePressure = command.AtmospherePressure,
            Wind = Wind.From(command.Wind),
            CloudBaseInMeters = command.CloudBaseInMeters,
            CloudinessInPercent = command.CloudinessInPercent,
            HorizontalVisibilityInKilometer = command.HorizontalVisibilityInKilometer
        };

        forecastWeather.SetCreatedAt(DateTime.UtcNow);
        forecastWeather.SetUpdateAt(DateTime.UtcNow);

        return forecastWeather;
    }

    public static ICollection<ForecastWeather> From(IAddWeatherForecastFromExcelCommand command)
    {
        const int offset = 6;

        var getCell = (int i, int j, int k) => command.WeatherForecasts
            .GetSheetAt(i)
            .GetRow(j)
            .GetCell(k);

        var directionFromStringToEnum = (string value, int idx) =>
        {// it is bad code
            var directions = value.Split(',');

            switch(directions[idx])
            {
                case "Ю": return Direction.South;
                case "С": return Direction.South;
                case "З": return Direction.South;
                case "В": return Direction.South;
                case "ЮВ": return Direction.South;
                case "ЮЗ": return Direction.South;
                case "СЗ": return Direction.South;
                case "СВ": return Direction.South;
                default: return Direction.Calm;
            }
        };

        var countRecords = 0;

        for (int i = 0; i < command.WeatherForecasts.NumberOfNames; ++i) countRecords += command.WeatherForecasts.GetSheetAt(i).LastRowNum - offset - 1;

        List<ForecastWeather> forecastWeathers = new(command.WeatherForecasts.NumberOfSheets);

        for (int i = 0; i < command.WeatherForecasts.NumberOfSheets; ++i)
            for (int j = offset; j < command.WeatherForecasts.NumberOfSheets; ++j)
                for (int k = 0; k < command.WeatherForecasts.NumberOfSheets; ++k)
                {
                    forecastWeathers[i] = new ForecastWeather()
                    {
                        DateWeatherEvent = getCell(i, j, k).DateCellValue,
                        CityName = command.CityName,
                        Temperature = getCell(i, j, k).NumericCellValue,
                        HumidityInPercent = getCell(i, j, k).NumericCellValue,
                        DewPoint = getCell(i, j, k).NumericCellValue,
                        AtmospherePressure = (int)getCell(i, j, k).NumericCellValue,
                        Wind = new Wind()
                        {
                            DirectionFirst = directionFromStringToEnum(getCell(i, j, k).StringCellValue, 0),
                            DirectionSecond = directionFromStringToEnum(getCell(i, j, k).StringCellValue, 1)
                        },
                        CloudinessInPercent = getCell(i, j, k).NumericCellValue,
                        CloudBaseInMeters = (int)getCell(i, j, k).NumericCellValue,
                        HorizontalVisibilityInKilometer = (int)getCell(i, j, k).NumericCellValue,
                        WeatherEvent = getCell(i, j, k).StringCellValue,
                    };

                    forecastWeathers[i].SetCreatedAt(DateTime.UtcNow);
                    forecastWeathers[i].SetUpdateAt(DateTime.UtcNow);
                }

        return forecastWeathers;
    }

    public DateTime DateWeatherEvent { get; private set; }
    public string CityName { get; private set; }
    public double Temperature { get; private set; }
    public double HumidityInPercent { get => HumidityInPercent; set => _ = value <= 100 ? value : 100; }
    public double DewPoint { get; private set; }
    public int AtmospherePressure { get; private set; }
    public Wind Wind { get; private set; }
    public double CloudinessInPercent { get => CloudinessInPercent; set => _ = value <= 100 ? value : 100; }
    public int CloudBaseInMeters { get; private set; }
    public int HorizontalVisibilityInKilometer { get; private set; }
    public string WeatherEvent { get; private set; }

    public void Change(IChangeWeatherForecastCommand command)
    {
        DateWeatherEvent = command.DateWeatherEvent;
        CityName = command.CityName;
        Temperature = command.Temperature;
        HumidityInPercent = command.HumidityInPercent;
        DewPoint = command.DewPoint;
        AtmospherePressure = command.AtmospherePressure;
        WeatherEvent = command.WeatherEvent;
        Wind = Wind.From(command.Wind);
        CloudinessInPercent = command.CloudinessInPercent;
        CloudBaseInMeters = command.CloudBaseInMeters;
        HorizontalVisibilityInKilometer = command.HorizontalVisibilityInKilometer;
        WeatherEvent = command.WeatherEvent;
    }
}