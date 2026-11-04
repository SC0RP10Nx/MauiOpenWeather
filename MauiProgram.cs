using MauiOpenWeather.Services;
using Microsoft.Extensions.Logging;

namespace MauiOpenWeather
{
    public static class MauiProgram
    {
        const string URL = "https://iot.web2teach.de";
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Wetter.Service einbinden
            builder.Services.AddSingleton<BFW_WeatherService>(p => new BFW_WeatherService(URL, Secrets.ApiKey));
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
