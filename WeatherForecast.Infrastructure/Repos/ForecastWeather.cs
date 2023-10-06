using Microsoft.EntityFrameworkCore;

using Dotseed.Domain;

using WeatherForecast.Domain.Aggregates.ForecastWeather;


namespace WeatherForecast.Infrastructure;

public class ForecastWeatherRepo : IForecastWeatherRepo
{
    private readonly Context _db;

    public ForecastWeatherRepo(Context db) => _db = db;

    public IUnitOfWork UnitOfWork => _db;


    public async Task AddAsync(ForecastWeather forecastWeather) => await _db.ForecastsWeather.AddAsync(forecastWeather);


    public async Task<ICollection<ForecastWeather>> FetchAsync(int offset, int size) => await _db.ForecastsWeather
        .Skip(offset)
        .Take(size)
        .OrderBy(b => b)
        .ToListAsync();
     

    public async Task<ForecastWeather> FindByIdAsync(Guid Id) => await _db.ForecastsWeather.FirstOrDefaultAsync(fw => fw.Id == Id);


    public async Task RemoveByIdAsync(Guid Id)
    {
        var book =  await _db.ForecastsWeather.FirstOrDefaultAsync(b => b.Id == Id);

        if (book is not null) _db.ForecastsWeather.Remove(book);
    }
}