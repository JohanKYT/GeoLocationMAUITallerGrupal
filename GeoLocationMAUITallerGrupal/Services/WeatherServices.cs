using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLocationMAUITallerGrupal.Models;

namespace GeoLocationMAUITallerGrupal.Services
{
    public class WeatherServices
    {
        public async Task<WeatherData> GetWeatherDataFromLocationAsync(double latitude, double longitude)
        {
            string latitude_str = latitude.ToString().Replace(",",".");
            string longitude_str = longitude.ToString().Replace(",",".");
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude_str}&longitude={longitude_str}&current_weather=true&timezone=America%2FLima";
            using ( HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

            }


        }
    }
}
