using Dotseed.Domain;
using MathNet.Numerics;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.UserModel;
using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Commands;
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
            DateWeatherEvent = DateTime.SpecifyKind(command.DateWeatherEvent, DateTimeKind.Utc),
            Temperature = command.Temperature,
            HumidityInPercent = command.HumidityInPercent,
            DewPoint = command.DewPoint,
            AtmospherePressure = command.AtmospherePressure,
            Wind = Wind.From(command.Wind),
            CloudBaseInMeters = command.CloudBaseInMeters,
            CloudinessInPercent = command.CloudinessInPercent,
            HorizontalVisibilityInKilometer = command.HorizontalVisibilityInKilometer,
            WeatherEvent = command.WeatherEvent
        };

        forecastWeather.SetCreatedAt(DateTime.UtcNow);
        forecastWeather.SetUpdateAt(DateTime.UtcNow);

        return forecastWeather;
    }

    public static LinkedList<ForecastWeather> From(IAddWeatherForecastFromExcelCommand command)
    {//Welcome to shit code))
        ICell bufferCell = null;

        var getCell = (int i, int j, ISheet sheet) => bufferCell = sheet
            .GetRow(i)
            .GetCell(j);

        var directionFromStringToEnum = (string value, int idx) =>
        {// it is bad code
            var directions = value.Split(',');

            if (idx >= 1 && directions.Length is 1) return Direction.Calm;

            switch (directions[idx])
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

        var getTimeInMinutes = (string value) =>
        {
            var hoursAndMinutes = value.Split(":");
            var hours = hoursAndMinutes[0];
            var minutes = hoursAndMinutes[1];

            return int.Parse(hours) * 60 + int.Parse(minutes);
        };

        LinkedList<ForecastWeather> forecastWeathers = new();

        const int offset = 6;

        for (int i = 0; i < command.WeatherForecasts.NumberOfSheets; ++i)
        {
            var sheet = command.WeatherForecasts.GetSheetAt(i);

            for (int j = offset; j < sheet.LastRowNum; ++j)
            {
                forecastWeathers.AddLast(new ForecastWeather()
                {
                    Id = Guid.NewGuid(),

                    DateWeatherEvent = DateTime.SpecifyKind(DateTime.Parse(getCell(j, 0, sheet).StringCellValue), DateTimeKind.Utc),

                    CityName = command.CityName,

                    Temperature = getCell(j, 2, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    HumidityInPercent = getCell(j, 3, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    DewPoint = getCell(j, 4, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    AtmospherePressure = getCell(j, 5, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    Wind = Wind.From(new WindModel(
                        getCell(j, 7, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,
                        directionFromStringToEnum(getCell(j, 6, sheet).StringCellValue, 0),
                        directionFromStringToEnum(getCell(j, 6, sheet).StringCellValue, 1)
                    )),

                    CloudinessInPercent = getCell(j, 8, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    CloudBaseInMeters = getCell(j, 9, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    HorizontalVisibilityInKilometer = getCell(j, 10, sheet).CellType is CellType.String ? 0 : (int)bufferCell.NumericCellValue,

                    WeatherEvent = getCell(j, 11, sheet)  is not null? bufferCell.StringCellValue: " ",
                });

                forecastWeathers.ElementAt(i).DateWeatherEvent.AddMinutes(getTimeInMinutes(getCell(j, 1, sheet).StringCellValue));
                forecastWeathers.ElementAt(i).SetCreatedAt(DateTime.UtcNow);
                forecastWeathers.ElementAt(i).SetUpdateAt(DateTime.UtcNow);
            }
        }

        return forecastWeathers;
    }

    public DateTime DateWeatherEvent { get; private set; }
    public string CityName { get; private set; }
    public double Temperature { get; private set; }
    public int HumidityInPercent { get; private set; }
    public double DewPoint { get; private set; }
    public int AtmospherePressure { get; private set; }
    public Wind Wind { get; private set; }
    public int CloudinessInPercent { get; private set; }
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

    private record WindModel(
        double SpeedWindInMetersPerSecond,
        Direction DirectionFirst,
        Direction DirectionSecond
    )
    : IAddWindValueCommand;
}


