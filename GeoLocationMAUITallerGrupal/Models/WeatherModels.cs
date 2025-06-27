using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeoLocationMAUITallerGrupal.Models
{
    public class WeatherData
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("generationtime_ms")]
        public double GenerationtimeMs { get; set; }

        [JsonProperty("utc_offset_seconds")]
        public double UtcOffsetSeconds { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty("elevation")]
        public double Elevation { get; set; }

        [JsonProperty("current_units")]
        public CurrentUnits CurrentUnits { get; set; }

        [JsonProperty("current")]
        public CurrentWeather Current { get; set; }
    }

    public class CurrentUnits
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public string Temperature2m { get; set; }

        [JsonProperty("relative_humidity_2m")]
        public string RelativeHumidity2m { get; set; }

        [JsonProperty("rain")]
        public string Rain { get; set; }
    }

    public class CurrentWeather
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("interval")]
        public double Interval { get; set; }

        [JsonProperty("temperature_2m")]
        public double Temperature2m { get; set; }

        [JsonProperty("relative_humidity_2m")]
        public double RelativeHumidity2m { get; set; }

        [JsonProperty("rain")]
        public double Rain { get; set; }
    }

}
