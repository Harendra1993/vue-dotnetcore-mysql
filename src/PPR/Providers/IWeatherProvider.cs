using System.Collections.Generic;
using PPR.Models;

namespace PPR.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
