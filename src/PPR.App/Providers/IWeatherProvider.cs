using System.Collections.Generic;
using PPR.App.DTOs;

namespace PPR.App.Providers {
    public interface IWeatherProvider {
        List<WeatherForecast> GetForecasts ();
    }
}
