using Microsoft.Extensions.DependencyInjection;

namespace MauiOpenWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            UserAppTheme = (AppTheme)Preferences.Get("theme", (int)AppTheme.Unspecified);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}