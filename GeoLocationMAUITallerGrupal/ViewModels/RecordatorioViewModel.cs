using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GeoLocationMAUITallerGrupal.Models;
using GeoLocationMAUITallerGrupal.Services;

namespace GeoLocationMAUITallerGrupal.ViewModels
{
    public class RecordatoriosViewModel : INotifyPropertyChanged
    {
        private readonly RecordatorioServices _recordatoriosService;
        private ObservableCollection<Recordatorio> _recordatorios = new();
        private bool _isLoading;
        private string _nuevoTexto = string.Empty;
        private DateTime _nuevaFecha = DateTime.Now.AddHours(1);
        private TimeSpan _nuevaHora = DateTime.Now.AddHours(1).TimeOfDay;

        public ObservableCollection<Recordatorio> Recordatorios
        {
            get => _recordatorios;
            set
            {
                _recordatorios = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string NuevoTexto
        {
            get => _nuevoTexto;
            set
            {
                _nuevoTexto = value;
                OnPropertyChanged();
                ((Command)AgregarRecordatorioCommand).ChangeCanExecute();
            }
        }

        public DateTime NuevaFecha
        {
            get => _nuevaFecha;
            set
            {
                _nuevaFecha = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan NuevaHora
        {
            get => _nuevaHora;
            set
            {
                _nuevaHora = value;
                OnPropertyChanged();
            }
        }

        public ICommand AgregarRecordatorioCommand { get; }
        public ICommand EliminarRecordatorioCommand { get; }
        public ICommand ToggleActivoCommand { get; }
        public ICommand RefreshCommand { get; }

        public RecordatoriosViewModel()
        {
            _recordatoriosService = new RecordatorioServices();

            AgregarRecordatorioCommand = new Command(async () => await AgregarRecordatorio(), () => !string.IsNullOrWhiteSpace(NuevoTexto));
            EliminarRecordatorioCommand = new Command<Recordatorio>(async (r) => await EliminarRecordatorio(r));
            ToggleActivoCommand = new Command<Recordatorio>(async (r) => await ToggleActivo(r));
            RefreshCommand = new Command(async () => await CargarRecordatorios());

            _ = CargarRecordatorios();
        }

        private async Task CargarRecordatorios()
        {
            try
            {
                IsLoading = true;
                var lista = await _recordatoriosService.GetRecordatoriosAsync();
                Recordatorios = new ObservableCollection<Recordatorio>(lista.OrderBy(r => r.FechaHora));
            }
            catch (Exception ex)
            {
                await MostrarError("Error al cargar recordatorios", ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AgregarRecordatorio()
        {
            try
            {
                var fechaHoraCompleta = NuevaFecha.Date + NuevaHora;

                if (fechaHoraCompleta <= DateTime.Now)
                {
                    await MostrarError("La fecha y hora deben ser futuras.");
                    return;
                }

                var nuevoRecordatorio = new Recordatorio
                {
                    Texto = NuevoTexto.Trim(),
                    FechaHora = fechaHoraCompleta,
                    Activo = true
                };

                var exito = await _recordatoriosService.AddRecordatorioAsync(nuevoRecordatorio);

                if (exito)
                {
                    Recordatorios.Add(nuevoRecordatorio);
                    OrdenarRecordatorios();
                    LimpiarCampos();
                }
                else
                {
                    await MostrarError("No se pudo agregar el recordatorio");
                }
            }
            catch (Exception ex)
            {
                await MostrarError("Error al agregar recordatorio", ex);
            }
        }

        private async Task EliminarRecordatorio(Recordatorio recordatorio)
        {
            try
            {
                bool confirmar = await Application.Current.MainPage.DisplayAlert(
                    "Confirmar", $"¿Eliminar el recordatorio: {recordatorio.Texto}?", "Sí", "No");

                if (confirmar)
                {
                    var exito = await _recordatoriosService.DeleteRecordatorioAsync(recordatorio.Id);
                    if (exito)
                        Recordatorios.Remove(recordatorio);
                    else
                        await MostrarError("No se pudo eliminar el recordatorio");
                }
            }
            catch (Exception ex)
            {
                await MostrarError("Error al eliminar recordatorio", ex);
            }
        }

        private async Task ToggleActivo(Recordatorio recordatorio)
        {
            try
            {
                recordatorio.Activo = !recordatorio.Activo;
                await _recordatoriosService.UpdateRecordatorioAsync(recordatorio);
                OrdenarRecordatorios();
            }
            catch (Exception ex)
            {
                await MostrarError("Error al actualizar recordatorio", ex);
            }
        }

        private void OrdenarRecordatorios()
        {
            Recordatorios = new ObservableCollection<Recordatorio>(
                Recordatorios.OrderBy(r => r.FechaHora)
            );
        }

        private void LimpiarCampos()
        {
            NuevoTexto = "";
            NuevaFecha = DateTime.Now.AddHours(1);
            NuevaHora = DateTime.Now.AddHours(1).TimeOfDay;
        }

        private async Task MostrarError(string mensaje, Exception ex = null)
        {
            string detalle = ex != null ? $"{mensaje}: {ex.Message}" : mensaje;
            await Application.Current.MainPage.DisplayAlert("Error", detalle, "OK");
        }

        //Se Obtuvo de chaGPT manejar los errores 

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

