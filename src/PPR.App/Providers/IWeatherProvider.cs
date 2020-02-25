using System.Collections.Generic;
using PPR.App.Models;

namespace PPR.App.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
