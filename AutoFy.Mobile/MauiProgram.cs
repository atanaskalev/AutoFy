using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Implementations;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Mobile.ViewModels;
using AutoFy.Mobile.Views;
using AutoFy.Services.Interfaces;
using AutoFy.Services.Services;
using AutoFy.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;

namespace AutoFy.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Database
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "autofy.db");

        builder.Services.AddDbContext<AutoFyDbContext>(options =>
        {
            options.UseSqlite($"Filename={dbPath}");
        });

        // Shell
        builder.Services.AddSingleton<AppShell>();

        // Repositories
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IFuelEntryRepository, FuelEntryRepository>();
        builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
        builder.Services.AddScoped<IServiceRecordRepository, ServiceRecordRepository>();

        // Services
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<IFuelService, FuelService>();
        builder.Services.AddScoped<IReminderService, ReminderService>();
        builder.Services.AddScoped<IServiceRecordService, ServiceRecordService>();
        builder.Services.AddScoped<IHistoryService, HistoryService>();
        builder.Services.AddScoped<IStatisticsService, StatisticsService>();

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
        builder.Services.AddTransient<ServiceHistoryView>();

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
        builder.Services.AddTransient<ServiceHistoryViewModel>();

        var app = builder.Build();

        // Create database automatically
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<AutoFyDbContext>();

        dbContext.Database.Migrate();

        return app;
    }
}