using System.Collections.Generic;
using VueDotNetMySql.App.DTOs;

namespace VueDotNetMySql.App.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
