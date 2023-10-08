using NPOI.SS.UserModel;

namespace WeatherForecast.Domain.Aggregates.WeatherForecast.Commands;

public interface IAddWeatherForecastFromExcelCommand
{
    public string CityName { get; }

    public IWorkbook WeatherForecasts { get; }
}
