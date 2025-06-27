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
        public WeatherData WeatherDataInfo
        {
            get => _weatherData;
            set
            {
                if (_weatherData != value)
                {
                    _weatherData = value;
                    OnPropertyChanged();
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
          
            RecalculateWeather = new Command(async () => WeatherFromLocation());
            GetCurrentWeatherFromLocation();
        }

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
