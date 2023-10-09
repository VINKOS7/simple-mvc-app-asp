using Dotseed.Domain;

namespace WeatherForecast.Domain.Aggregates.ForecastWeather;

public interface IForecastWeatherRepo : IRepository<ForecastWeather>
{
    public Task AddAsync(ForecastWeather book);

    public Task AddNotDoubleByDateAsync(ForecastWeather forecastWeather);

    public Task RemoveByIdAsync(Guid id);

    public Task<ForecastWeather> FindByIdAsync(Guid Id);

    public Task<ICollection<ForecastWeather>> FetchAsync(int offset, int size);
}
