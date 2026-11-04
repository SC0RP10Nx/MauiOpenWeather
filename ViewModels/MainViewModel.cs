using MauiOpenWeather.Models;
using MauiOpenWeather.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiOpenWeather.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly BFW_WeatherService _weatherService;
        private BFW_WeatherResponse? _weatherResponse;
        private bool _isBusy;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel(BFW_WeatherService weatherService)
        {
            _weatherService = weatherService;
            RefreshCommand = new Command(async () => await UpdateWeather());
        }
        // Befehle
        public ICommand RefreshCommand { get; }

        public bool IsBusy
        {
            get => _isBusy;
            private set { _isBusy = value; OnPropertyChanged(); }
        }
        // Wetterdaten
        public BFW_WeatherResponse? WeatherResponse
        {
            get => _weatherResponse;
            private set
            {
                _weatherResponse = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Current));
                OnPropertyChanged(nameof(WindDirectionDe));
                OnPropertyChanged(nameof(WindArrowRotation));
                OnPropertyChanged(nameof(UpdatedText));
                OnPropertyChanged(nameof(SceneLabel));
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }
        public Item? Current => WeatherResponse?.Items?.FirstOrDefault();
        // Windrichtungen
        private static readonly string[] Directions = [ "N","NNE","NE","ENE","E","ESE","SE","SSE",
            "S","SSW","SW","WSW","W","WNW","NW","NNW" ];

        // Windrichtung in Deutsch
        public string WindDirectionDe => (Current?.WindDirection ?? "").ToUpper().Replace("E", "O");

        // Windrichtung Pfeil

        public double WindArrowRotation
        {
            get
            {
                int i = Array.IndexOf(Directions, Current?.WindDirection?.ToUpper());
                return i < 0 ? 0 : (i * 22.5 + 180) % 360; // Add 180 degrees to point the arrow in the correct direction
            }
        }

        // Zeitstempel der letzten Aktualisierung
        public string UpdatedText => Current == null ? "" : $"{Current.Timestamp:HH:mm} Uhr";

        // Jahreszeit + Wetter
        private string _season = DateTime.Now.Month switch
        {
            3 or 4 or 5 => "fruehling",
            6 or 7 or 8 => "sommer",
            9 or 10 or 11 => "herbst",
            _ => "winter"
        };

        private string _condition =>
            Current == null ? "sonne" :
            Current.Rain > 0 ? "regen" :
            Current.Humidity >= 85 ? "wolken" :
            "sonne";
        public string BackgroundImage => $"bg_{_season}_{_condition}.jpg";

        public string SceneLabel
        {
            get
            {
                string s = _season switch
                {
                    "fruehling" => "Frühling",
                    "sommer" => "Sommer",
                    "herbst" => "Herbst",
                    _ => "Winter"
                };
                string c = _condition switch
                {
                    "regen" => _season == "winter" ? "Schnee" : "Regen",
                    "wolken" => "Bewölkt",
                    _ => "Sonnig"
                };
                return $"{s}: {c}";
            }
        }

        // Laden der Wetterdaten
        public async Task<bool> UpdateWeather()
        {
            if (IsBusy) return false;
            IsBusy = true;
            try
            {
                WeatherResponse = await _weatherService.GetAsync("iot-outdoor-data.php");
                return true;
            }
            catch (Exception ex)
            {
                WeatherResponse = new BFW_WeatherResponse() { ErrMsg = ex.Message, Success = false };
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
