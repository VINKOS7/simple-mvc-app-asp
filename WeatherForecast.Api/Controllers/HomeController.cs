using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();


    public IActionResult Privacy() => View();


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
}