using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GeoLocationMAUITallerGrupal.Models;
using GeoLocationMAUITallerGrupal.Services;

namespace GeoLocationMAUITallerGrupal.ViewModels
{
    internal class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weatherData= new WeatherData();
        private string _weatherImage;
        public string WeatherImage
        {
            get => _weatherImage;
            set
            {
                if (_weatherImage != value)
                {
                    _weatherImage = value;
                    OnPropertyChanged();
                }
            }
        }
        public WeatherData WeatherDataInfo
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TemperaturaConUnidad));
                    OnPropertyChanged(nameof(HumedadConUnidad));
                    OnPropertyChanged(nameof(LluviaConUnidad));
                    SetWeatherImage();
                }
            }
        }
        public ICommand RecalculateWeather{get; set;}

        public WeatherViewModel()
        {
            WeatherDataInfo = new WeatherData
            {
                Current = new CurrentWeather(),
                CurrentUnits = new CurrentUnits()
            };
            WeatherImage = "dotnet_bot.png"; // imagen por defecto
            RecalculateWeather = new Command(async () => WeatherFromLocation());
            GetCurrentWeatherFromLocation();
        }
        private void SetWeatherImage()
        {
            var temp = WeatherDataInfo?.Current?.Temperature2m ?? 0;
            var rain = WeatherDataInfo?.Current?.Rain ?? 0;

            if (rain > 0)
                WeatherImage = "lluvia.png";
            else if (temp >= 25)
                WeatherImage = "soleado.png";
            else if (temp >= 5)
                WeatherImage = "nublado.png";
            else
                WeatherImage = "frio.png";
        }

        public string TemperaturaConUnidad =>
            $"{WeatherDataInfo?.Current?.Temperature2m ?? 0} {WeatherDataInfo?.CurrentUnits?.Temperature2m ?? "°C"}";

        public string HumedadConUnidad =>
            $"{WeatherDataInfo?.Current?.RelativeHumidity2m ?? 0} {WeatherDataInfo?.CurrentUnits?.RelativeHumidity2m ?? "%"}";

        public string LluviaConUnidad =>
            $"{WeatherDataInfo?.Current?.Rain ?? 0} {WeatherDataInfo?.CurrentUnits?.Rain ?? "mm"}";

        public async void GetCurrentWeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDataInfo = await weatherServices.GetCurrentLocationWeatherAsync();
        }

        public async void WeatherFromLocation()
        {
            WeatherServices weatherServices = new WeatherServices();
            WeatherDataInfo = await weatherServices.GetWeatherDataFromLocationAsync(_weatherData.Latitude, _weatherData.Longitude);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        
    }
}
