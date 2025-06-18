using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationMAUITallerGrupal.Models
{
    public class WeatherData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double GenerationtimeMs { get; set; }
        public double UtcOffsetSeconds { get; set; }
        public string Timezone { get; set; }
        public string TimezoneAbbreviation { get; set; }
        public double Elevation { get; set; }
        public CurrentUnits CurrentUnits { get; set; }
        public CurrentWeather Current { get; set; }
    }

    public class CurrentUnits
    {
        public string Time { get; set; }
        public string Interval { get; set; }
        public string Temperature2m { get; set; }
        public string RelativeHumidity2m { get; set; }
        public string Rain { get; set; }
    }

    public class CurrentWeather
    {
        public string Time { get; set; }
        public double Interval { get; set; }
        public double Temperature2m { get; set; }
        public double RelativeHumidity2m { get; set; }
        public double Rain { get; set; }
    }

}
