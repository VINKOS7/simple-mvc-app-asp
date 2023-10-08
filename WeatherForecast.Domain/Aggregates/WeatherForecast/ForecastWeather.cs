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
    {//Welcome to shit code))
        ICell bufferCell = null;

        var getCell = (int i, int j, int k) =>
        {
            bufferCell = command.WeatherForecasts
            .GetSheetAt(i)
            .GetRow(j)
            .GetCell(k);

            return bufferCell;
        };

        


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
            for (int j = offset; j < command.WeatherForecasts.GetSheetAt(i).LastRowNum; ++j)
            {
                //i know, is bad
                forecastWeathers.AddLast(new ForecastWeather()
                {
                    DateWeatherEvent = DateTime.Parse(getCell(i, j, 0).StringCellValue),

                    CityName = command.CityName,

                    Temperature = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    HumidityInPercent = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    DewPoint = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    AtmospherePressure = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    Wind = Wind.From(new WindModel(
                        getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,
                        directionFromStringToEnum(getCell(i, j, 6).StringCellValue, 0), 
                        directionFromStringToEnum(getCell(i, j, 6).StringCellValue, 1)
                    )),

                    CloudinessInPercent = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    CloudBaseInMeters = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    HorizontalVisibilityInKilometer = getCell(i, j, 10).CellType is CellType.String ? 0 : (int) bufferCell.NumericCellValue,

                    WeatherEvent = getCell(i, j, 11).StringCellValue,
                });

                forecastWeathers.ElementAt(i).DateWeatherEvent.AddMinutes(getTimeInMinutes(getCell(i, j, 1).StringCellValue));
                forecastWeathers.ElementAt(i).SetCreatedAt(DateTime.UtcNow);
                forecastWeathers.ElementAt(i).SetUpdateAt(DateTime.UtcNow);
            }

        return forecastWeathers;
    }

    public DateTime DateWeatherEvent { get; private set; }
    public string CityName { get; private set; }
    public double Temperature { get; private set; }
    public int HumidityInPercent { get => HumidityInPercent; set => _ = value <= 100 ? value > 0 ? value : 0 : 100; }
    public double DewPoint { get; private set; }
    public int AtmospherePressure { get; private set; }
    public Wind Wind { get; private set; }
    public int CloudinessInPercent { get => CloudinessInPercent; set => _ = value <= 100 ? value > 0 ? value : 0 : 100; }
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
    ) : IAddWindValueCommand;
}

