using MediatR;
using Newtonsoft.Json;
using NPOI.SS.UserModel;

using WeatherForecast.Domain.Aggregates.ForecastWeather.Commands;
using WeatherForecast.Api.Responses;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Commands;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Values.WindValue.Enums;
using WeatherForecast.Domain.Aggregates.WeatherForecast.Commands;
using NPOI.XSSF.UserModel;

namespace WeatherForecast.Api.Requests;

public record AddWeatherForecastFromExelRequest(
     [JsonProperty("cityName")] string CityName,
     [JsonProperty("weatherForecasts")] IFormFile WeatherForecasts
)
: IAddWeatherForecastFromExcelCommand, IRequest<IEnumerable<Guid>>
{
    IWorkbook IAddWeatherForecastFromExcelCommand.WeatherForecasts => new XSSFWorkbook(WeatherForecasts is not null ? WeatherForecasts.OpenReadStream() : throw new BadHttpRequestException("file is empty"));
}

public record AddWeatherForecastRequest(
    [JsonProperty("dateWeatherEvent")] DateTime DateWeatherEvent,
    [JsonProperty("cityName")] string CityName,
    [JsonProperty("temperature")] double Temperature,
    [JsonProperty("humidityInPercent")] int HumidityInPercent,
    [JsonProperty("dewPoint")] double DewPoint,
    [JsonProperty("atmospherePressure")] int AtmospherePressure,
    [JsonProperty("wind")] WindRequestModel Wind,
    [JsonProperty("cloudinessInPercent")] int CloudinessInPercent,
    [JsonProperty("cloudBaseInMeters")] int CloudBaseInMeters,
    [JsonProperty("horizontalVisibilityInKilometer")] int HorizontalVisibilityInKilometer,
    [JsonProperty("weatherEvent")] string WeatherEvent
)
: IAddWeatherForecastCommand, IRequest<Guid>
{
    IAddWindValueCommand IAddWeatherForecastCommand.Wind => Wind;
}

public record ChangeWeatherForecastRequest(
    [JsonProperty("id")] Guid Id,
    [JsonProperty("dateWeatherEvent")] DateTime DateWeatherEvent,
    [JsonProperty("cityName")] string CityName,
    [JsonProperty("temperature")] double Temperature,
    [JsonProperty("humidityInPercent")] int HumidityInPercent,
    [JsonProperty("dewPoint")] double DewPoint,
    [JsonProperty("atmospherePressure")] int AtmospherePressure,
    [JsonProperty("wind")] WindRequestModel Wind,
    [JsonProperty("cloudinessInPercent")] int CloudinessInPercent,
    [JsonProperty("cloudBaseInMeters")] int CloudBaseInMeters,
    [JsonProperty("horizontalVisibilityInKilometer")] int HorizontalVisibilityInKilometer,
    [JsonProperty("weatherEvent")] string WeatherEvent
)
: IChangeWeatherForecastCommand, IRequest
{
    IAddWindValueCommand IAddWeatherForecastCommand.Wind => Wind;
}


public record FetchWeatherForecastsRequest(
    [JsonProperty("offset")] int Offset,
    [JsonProperty("size")] int Size
) 
: IRequest<FetchWeatherForecastsResponse>;

public record DeleteWeatherForecastByIdRequest([JsonProperty("id")] Guid Id) : IRequest;

public record WindRequestModel(
    [JsonProperty("speedWindInMetersPerSecond")] double SpeedWindInMetersPerSecond,
    [JsonProperty("directionFirst")] Direction DirectionFirst,
    [JsonProperty("directionSecond")] Direction DirectionSecond
)
: IAddWindValueCommand;