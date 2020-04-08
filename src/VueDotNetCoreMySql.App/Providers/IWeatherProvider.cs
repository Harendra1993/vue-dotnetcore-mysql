using System.Collections.Generic;
using VueDotNetCoreMySql.App.DTOs;

namespace VueDotNetCoreMySql.App.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
