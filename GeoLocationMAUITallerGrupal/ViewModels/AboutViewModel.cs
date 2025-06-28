using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeoLocationMAUITallerGrupal.Models;

namespace GeoLocationMAUITallerGrupal.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EquipoGrupal> _equipoGrupal;

        public ObservableCollection<EquipoGrupal> Equipogrupal
        {
            get => _equipoGrupal;
            set
            {
                if (_equipoGrupal != value)
                {
                    _equipoGrupal = value;
                    OnPropertyChanged();
                }
            }
        }

        public AboutViewModel()
        {
            LoadEquipoGrupal();
        }

        private void LoadEquipoGrupal()
        {
            Equipogrupal = new ObservableCollection<EquipoGrupal>
            {
                new EquipoGrupal
                {
                    Name = "Juan Antamba",
                    Age = 20,
                    FavoriteSport = "Fútbol",
                    Description = "Me gusta la programación frontend"
                },
                new EquipoGrupal
                {
                    Name = "Rodrigo Andrade",
                    Age = 29,
                    FavoriteSport = "Nadar",
                    Description = "Yolo"
                },
                new EquipoGrupal
                {
                    Name = "Kevin Maquis",
                    Age = 26,
                    FavoriteSport = "Futbol y Baquet",
                    Description = "Me gusta jugar videojuegos, y averiguar acerca de varias tecnologias, tambien juego futbol en equipos barriales."
                },

            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
