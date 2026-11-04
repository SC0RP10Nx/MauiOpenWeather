using MauiOpenWeather.Services;
using MauiOpenWeather.ViewModels;

namespace MauiOpenWeather
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

        public MainPage(BFW_WeatherService weatherService)
        {
            InitializeComponent();
            viewModel = new MainViewModel(weatherService);
            BindingContext = viewModel;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.UpdateWeather();
        }

        private void OnThemeToggleClicked(object sender, EventArgs e)
        {
            var next = Application.Current!.RequestedTheme == AppTheme.Dark ? AppTheme.Light : AppTheme.Dark;
            Application.Current.UserAppTheme = next;
            Preferences.Set("theme", (int)next);
        }

    }
}
