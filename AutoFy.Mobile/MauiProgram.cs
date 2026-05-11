using AutoFy.Mobile.ViewModels;
using AutoFy.Mobile.Views;
using AutoFy.ViewModels;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace AutoFy.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.ConfigureSyncfusionCore();

            // Shell
            builder.Services.AddSingleton<AppShell>();

            // Views
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<CalendarView>();
            builder.Services.AddTransient<StatisticsView>();
            builder.Services.AddTransient<HistoryView>();
            builder.Services.AddTransient<SettingsView>();
            builder.Services.AddTransient<AboutView>();
            builder.Services.AddTransient<VehicleDetailsView>();
            builder.Services.AddTransient<AddVehicleView>();
            builder.Services.AddTransient<AddFuelEntryView>();
            builder.Services.AddTransient<FuelHistoryView>();
            builder.Services.AddTransient<AddReminderView>();
            builder.Services.AddTransient<AddServiceRecordView>();

            // ViewModels
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<CalendarViewModel>();
            builder.Services.AddTransient<StatisticsViewModel>();
            builder.Services.AddTransient<HistoryViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<AboutViewModel>();
            builder.Services.AddTransient<VehicleDetailsViewModel>();
            builder.Services.AddTransient<AddVehicleViewModel>();
            builder.Services.AddTransient<AddFuelEntryViewModel>();
            builder.Services.AddTransient<FuelHistoryViewModel>();
            builder.Services.AddTransient<AddReminderViewModel>();
            builder.Services.AddTransient<AddServiceRecordViewModel>();

            return builder.Build();
        }
    }
}
