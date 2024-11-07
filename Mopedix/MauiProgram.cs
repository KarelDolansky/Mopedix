using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace Mopedix
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register services
            builder.Services.AddSingleton<IBluetoothLE>(CrossBluetoothLE.Current);
            builder.Services.AddSingleton<IAdapter>(CrossBluetoothLE.Current.Adapter);
            builder.Services.AddSingleton<BleService>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}
