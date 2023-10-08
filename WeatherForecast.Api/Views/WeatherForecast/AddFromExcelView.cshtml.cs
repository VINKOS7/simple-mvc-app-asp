using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.Formula.Functions;
using WeatherForecast.Api.Requests;
using WeatherForecast.Domain.Aggregates.ForecastWeather;

namespace WeatherForecast.Api.Views.WeatherForecast;

public class AddFromExcelViewModel : PageModel
{
    [BindProperty]
    public AddWeatherForecastFromExelRequest Request { get; set; }
    public string Message { get; private set; } = "Импорт прогнозов";

    public void OnPost()
    {
        Message = "Импортировано";
    }
}
